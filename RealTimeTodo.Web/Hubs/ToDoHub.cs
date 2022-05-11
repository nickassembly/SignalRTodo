
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

       await Clients.Caller.SendAsync("updatedToDoList", results);
    }

    public async Task GetList(int listId)
    {
       var result = await todoRepository.GetList(listId);

       await Clients.Caller.SendAsync("updatedListData", result); 
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
        var groupName = $"list-updates-{listId}";
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

     public async Task UnsubscribeToListUpdates(int listId)
    {
         var groupName = $"list-updates-{listId}";
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task AddToDoList(int listId, string text)
    {
        await todoRepository.AddToDoItem(listId, text);

        // notify list count updates
        // notify list viewers on updates
        
    }

    // ToggleToDoItem 32:00


}