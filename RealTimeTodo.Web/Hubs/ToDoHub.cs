
using Microsoft.AspNetCore.SignalR;

public class ToDoHub : Hub
{
    private readonly IToDoRepository todoRepository;
    public ToDoHub(IToDoRepository todoRepository)
    {
        this.todoRepository = todoRepository;
    }

    public Task<List<ToDoList>> GetLists()
    {
        return todoRepository.GetLists();
    }

}