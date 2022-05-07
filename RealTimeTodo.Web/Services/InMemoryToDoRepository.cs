public class InMemoryToDoRepository : IToDoRepository
{
    private static List<ToDoList> Lists {get; set;} = new List<ToDoList>();

    static InMemoryToDoRepository()
    {
        Lists.Add(new ToDoList() { Id = 0, Name = "Foo", Items = new List<ToDoItem>()});
        Lists.Add(new ToDoList() { Id = 1, Name = "Bar", Items = new List<ToDoItem>()});
        Lists.Add(new ToDoList() { Id = 2, Name = "Test", Items = new List<ToDoItem>()});
        Lists.Add(new ToDoList() { Id = 3, Name = "Fail", Items = new List<ToDoItem>()});
    }

    public Task<List<ToDoList>> GetLists()
    {
       return Task.FromResult(Lists.Select(p => p.GetMinimal()));
    }
}

