using FluentValidation;
using MediatR;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Commands.Handlers;

public class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItem>
{
    private readonly IToDoRepository _repository;
    private readonly IValidator<ToDoItem> _validator;

    public UpdateToDoItemHandler(IToDoRepository repository, IValidator<ToDoItem> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(UpdateToDoItem request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemAsync(request.Id, cancellationToken);

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.DueDate = request.DueDate;
        entity.IsCompleted = request.IsCompleted;

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);
        await _repository.SaveChangesAsync(entity, cancellationToken);
    }
}