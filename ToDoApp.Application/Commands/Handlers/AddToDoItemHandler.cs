using FluentValidation;
using MediatR;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Infrastructure.Repositories;

namespace ToDoApp.Application.Commands.Handlers;

public class AddToDoItemHandler : IRequestHandler<AddToDoItem>
{
    private readonly IToDoRepository _repository;
    private readonly IValidator<ToDoItem> _validator;

    public AddToDoItemHandler(IToDoRepository repository, IValidator<ToDoItem> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(AddToDoItem request, CancellationToken cancellationToken)
    {
        var entity = new ToDoItem
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = false
        };

        await _validator.ValidateAndThrowAsync(entity, cancellationToken);
        await _repository.AddAsync(entity, cancellationToken);
    }
}