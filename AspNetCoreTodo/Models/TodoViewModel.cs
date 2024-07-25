namespace AspNetCoreTodo.Models;
public class TodoViewModel
{
    public TodoViewModel() : this(Array.Empty<TodoItem>())
    {

    }
    public TodoViewModel(TodoItem[] items)
    {
        Items = items;
    }
    public TodoItem[] Items { get; set; }
}
