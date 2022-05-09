
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

}