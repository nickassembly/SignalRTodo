public interface IToDoRepository
{
    Task<List<ToDoList>> GetLists();
    Task<ToDoList> GetList(int id);
    Task AddToDoItem(int listId, string text);
    Task ToggleToDoItem(int listId, int itemId);
}

