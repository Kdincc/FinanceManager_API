using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public sealed class UpdateIncomeTypeCommandValidator : AbstractValidator<UpdateIncomeTypeCommand>
    {
        public UpdateIncomeTypeCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxNameLength);
            RuleFor(p => p.Description).NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxDescriptionLength);
        }
    }
}
