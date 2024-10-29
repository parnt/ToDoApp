using MediatR;
using ToDoApp.Application.Mappers;
using ToDoApp.Application.Model;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Queries.Handlers;

public class GetUpcomingToDoItemsHandler : IRequestHandler<GetUpcomingToDoItems, IEnumerable<ToDoItemEntityDto>>
{
    private readonly IToDoRepository _repository;

    public GetUpcomingToDoItemsHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ToDoItemEntityDto>> Handle(GetUpcomingToDoItems _, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetUpcomingAsync(cancellationToken);

        var result = entities.Select(x => (ToDoItemEntityDto)new ToDoItemMapper(x));

        return result;
    }
}