using ToDoApp.Application.Model;
using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Application.Mappers;

public class ToDoItemMapper(ToDoItem entity)
{
    private ToDoItem Entity { get; } = entity;
    
    public static implicit operator ToDoItemEntityDto(ToDoItemMapper? item)
    {
        if (item?.Entity is null) return null!;
        
        return new ToDoItemEntityDto
        {
            Title = item.Entity.Title,
            Description = item.Entity.Description,
            DueDate = item.Entity.DueDate,
            Id = item.Entity.Id,
            IsCompleted = item.Entity.IsCompleted
        };
    }
}