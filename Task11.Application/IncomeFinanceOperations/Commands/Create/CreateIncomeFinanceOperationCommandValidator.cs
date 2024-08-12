using FluentValidation;
using Task11.Application.Properties;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public sealed class CreateIncomeFinanceOperationCommandValidator : AbstractValidator<CreateIncomeFinanceOperationCommand>
    {
        public CreateIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.FinanceOperation.DateFormat, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);

            RuleFor(x => x.IncomeTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);

            RuleFor(x => x.Amount)
                .Must(x => x >= 0)
                .WithMessage(ValidationErrorMessages.IncorrectAmountValue);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.FinanceOperation.MaxNameLength);
        }
    }
}
