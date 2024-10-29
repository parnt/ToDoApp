using MediatR;
using ToDoApp.Application.Model;

namespace ToDoApp.Application.Queries;

public record GetUpcomingToDoItems() : IRequest<IEnumerable<ToDoItemEntityDto>>;