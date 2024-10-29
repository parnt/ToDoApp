using MediatR;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Commands.Handlers;

public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItem>
{
    private readonly IToDoRepository _repository;

    public DeleteToDoItemHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteToDoItem request, CancellationToken cancellationToken)
    {
        await _repository.DeleteItemAsync(request.Id, cancellationToken);
    }
}