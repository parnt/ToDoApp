using FluentValidation;
using ToDoApp.Infrastructure.Entities;

namespace ToDoApp.Application.Validators;

public class ToDoItemValidator : AbstractValidator<ToDoItem>
{
    public ToDoItemValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.DueDate).NotNull().GreaterThanOrEqualTo(DateTime.Now);
    }
}