using FluentValidation;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandValidator : AbstractValidator<DeleteIncomeTypeCommand>
    {
        public DeleteIncomeTypeCommandValidator()
        {
            RuleFor(i => i.IncomeTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _));
        }
    }
}
