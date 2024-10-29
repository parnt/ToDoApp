using MediatR;
using ToDoApp.Application.Mappers;
using ToDoApp.Application.Model;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Queries.Handlers;

public class GetToDoItemHandler : IRequestHandler<GetToDoItem, ToDoItemEntityDto>
{
    private readonly IToDoRepository _repository;

    public GetToDoItemHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ToDoItemEntityDto> Handle(GetToDoItem request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemAsync(request.Id, cancellationToken);

        var result = new ToDoItemMapper(entity);

        return result;
    }
}