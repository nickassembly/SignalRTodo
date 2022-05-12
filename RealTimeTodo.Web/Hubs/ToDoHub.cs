
using Microsoft.AspNetCore.SignalR;

// 25:00

public class ToDoHub : Hub
{
    private readonly IToDoRepository todoRepository;
    public ToDoHub(IToDoRepository todoRepository)
    {
        this.todoRepository = todoRepository;
    }

    public async Task GetLists()
    {
        var results = await todoRepository.GetLists();

       await Clients.Caller.SendAsync("UpdatedToDoList", results);
    }

    public async Task GetList(int listId)
    {
       var result = await todoRepository.GetList(listId);

       await Clients.Caller.SendAsync("UpdatedListData", result); 
    } 

    public async Task SubscribeToCountUpdates()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "Counts");
    }

     public async Task UnsubscribeToCountUpdates()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Counts");
    }

     public async Task SubscribeToListUpdates(int listId)
    {
        var groupName = ListIdToGroupName(listId);
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

     public async Task UnsubscribeToListUpdates(int listId)
    {
         var groupName = ListIdToGroupName(listId);
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task AddToDoList(int listId, string text)
    {
       await todoRepository.AddToDoItem(listId, text);

       // notify list count updates
       var allLists = await todoRepository.GetLists();
       var listUpdate = await todoRepository.GetList(listId); 

       // notify list viewers on update
       var groupName = ListIdToGroupName(listId);
       await Clients.Group("Counts").SendAsync("UpdatedToDoList", allLists);
       await Clients.Group(groupName).SendAsync("UpdatedListData", listUpdate);
    }

    public async Task ToggleToDoItem(int listId, int itemId)
    {
       await todoRepository.ToggleToDoItem(listId, itemId);

       // notify list count updates
       var allLists = await todoRepository.GetLists();
       var listUpdate = await todoRepository.GetList(listId); 

       // notify list viewers on update
       var groupName = ListIdToGroupName(listId);
       await Clients.Group("Counts").SendAsync("UpdatedToDoList", allLists);
       await Clients.Group(groupName).SendAsync("UpdatedListData", listUpdate);
    }


    private string ListIdToGroupName(int listId) => $"list-updates-{listId}";


}