using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Update
{
    public sealed class UpdateExpenceFinanceOperationCommandValidator : AbstractValidator<UpdateExpenceFinanceOperationCommand>
    {
        public UpdateExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.ExpenceTypeId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .NotEmpty()
                .Must(x => x.Value >= 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.ExpenseFinanceOperation.MaxNameLength);

            RuleFor(x => x.ExpenseFinanceOperationId)
                .NotEmpty();
        }
    }
}
