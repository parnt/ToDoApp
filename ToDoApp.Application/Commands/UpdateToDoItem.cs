using MediatR;

namespace ToDoApp.Application.Commands;

public record UpdateToDoItem(int Id, string Title, string Description, DateTime DueDate, bool IsCompleted) : IRequest;