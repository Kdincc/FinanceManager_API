using FluentValidation;
using Task11.Application.Properties;
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
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);

            RuleFor(x => x.ExpenseTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);

            RuleFor(x => x.Amount)
                .NotEmpty()
                .Must(x => x >= 0)
                .WithMessage(ValidationErrorMessages.IncorrectAmountValue);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.ExpenseFinanceOperation.MaxNameLength);
        }
    }
}
