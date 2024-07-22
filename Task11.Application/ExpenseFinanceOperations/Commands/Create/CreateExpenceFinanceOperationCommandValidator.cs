using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Create
{
    public class CreateExpenceFinanceOperationCommandValidator : AbstractValidator<CreateExpenseFinanaceOperationCommand>
    {
        public CreateExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.ExpenseFinanceOperation.DateFormat, out _))
                .WithMessage("Incorrect date format, coorect format is yyyy-MM-DD");

            RuleFor(x => x.ExpenseTypeId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .NotEmpty()
                .Must(x => x.Value >= 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ExpenseFinanceOperation.MaxNameLength);
        }
    }
}
