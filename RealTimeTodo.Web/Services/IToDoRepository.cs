public interface IToDoRepository
{
    Task<List<ToDoList>> GetLists();
}

