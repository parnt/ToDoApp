namespace ToDoApp.Application.Model;

public class ToDoItemEntityDto : ToDoItemDto
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
}