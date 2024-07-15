using FluentValidation;
using Task11.Domain.Common.Сonstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseTypes.Commands.Create
{
    public sealed class CreateExpenseTypeCommandValidator : AbstractValidator<CreateExpenseTypeCommand>
    {
        public CreateExpenseTypeCommandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxNameLength);
            RuleFor(e => e.Description)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxDescriptionLength);
        }
    }
}
