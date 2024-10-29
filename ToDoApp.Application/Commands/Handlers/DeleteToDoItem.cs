using MediatR;

namespace ToDoApp.Application.Commands.Handlers;

public record DeleteToDoItem(int Id) : IRequest;