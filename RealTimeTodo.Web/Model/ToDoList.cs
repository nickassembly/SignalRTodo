public class ToDoList
{
    // 8:00 pt2
    public int Id {get; set;}
    public string Name {get; set;}
    public List<ToDoItem> Items {get; set;}
    public int Pending => Items.Count(p => !p.IsCompleted);
    public int Completed => Items.Count(p => p.IsCompleted);

    public ToDoListMinimal GetMinimal() 
    {
        return new ToDoListMinimal() {
            Id = Id,
            Name = Name,
            Pending = Pending,
            Completed = Completed
        };
    }
}