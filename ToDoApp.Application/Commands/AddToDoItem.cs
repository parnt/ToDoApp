using MediatR;

namespace ToDoApp.Application.Commands;

public record AddToDoItem(string Title, string Description, DateTime DueDate) : IRequest;