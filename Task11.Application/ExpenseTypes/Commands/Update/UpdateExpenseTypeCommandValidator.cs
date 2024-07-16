using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public sealed class UpdateExpenseTypeCommandValidator : AbstractValidator<UpdateExpenseTypeCommand>
    {
        public UpdateExpenseTypeCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxNameLength);

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxDescriptionLength);

            RuleFor(p => p.ExpenseTypeId)
                .NotEmpty();
        }
    }
}
