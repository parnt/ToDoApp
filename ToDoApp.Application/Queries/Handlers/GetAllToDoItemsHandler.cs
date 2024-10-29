using MediatR;
using ToDoApp.Application.Mappers;
using ToDoApp.Application.Model;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Queries.Handlers;

public class GetAllToDoItemsHandler : IRequestHandler<GetAllToDoItems, IEnumerable<ToDoItemEntityDto>>
{
    private readonly IToDoRepository _repository;

    public GetAllToDoItemsHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ToDoItemEntityDto>> Handle(GetAllToDoItems request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetItemsAsync(request.StartDate, cancellationToken);

        var result = entities.Select(x => (ToDoItemEntityDto)new ToDoItemMapper(x));

        return result;
    }
}