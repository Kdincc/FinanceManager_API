using FluentValidation;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.IncomeTypes.Commands.Update
{
    public sealed class UpdateIncomeTypeCommandValidator : AbstractValidator<UpdateIncomeTypeCommand>
    {
        public UpdateIncomeTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _));

            RuleFor(p => p.Name).NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxNameLength);

            RuleFor(p => p.Description).NotEmpty()
                .MaximumLength(ValidationConstants.OperationType.MaxDescriptionLength);
        }
    }
}
