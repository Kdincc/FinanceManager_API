using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public sealed class UpdateExpenceFinanceOperationCommandValidator : AbstractValidator<UpdateExpenceFinanceOperationCommand>
    {
        public UpdateExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.ExpenseFinanceOperation.DateFormat, out _))
                .WithMessage("Incorrect date format, coorect format is yyyy-MM-DD");

            RuleFor(x => x.ExpenceTypeId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .NotEmpty()
                .Must(x => x.Value >= 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ExpenseFinanceOperation.MaxNameLength);

            RuleFor(x => x.ExpenseFinanceOperationId)
                .NotEmpty();
        }
    }
}
