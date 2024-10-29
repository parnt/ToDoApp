using MediatR;
using ToDoApp.Application.Model;

namespace ToDoApp.Application.Queries;

public record GetAllToDoItems(DateTime StartDate) : IRequest<IEnumerable<ToDoItemEntityDto>>;