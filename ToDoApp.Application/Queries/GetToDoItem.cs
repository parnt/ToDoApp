using MediatR;
using ToDoApp.Application.Model;

namespace ToDoApp.Application.Queries;

public record GetToDoItem(int Id) : IRequest<ToDoItemEntityDto>;