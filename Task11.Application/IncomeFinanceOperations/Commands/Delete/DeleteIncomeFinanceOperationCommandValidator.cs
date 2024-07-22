using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Properties;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public sealed class DeleteIncomeFinanceOperationCommandValidator : AbstractValidator<DeleteIncomeFinanceOperationCommand>
    {
        public DeleteIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.IncomeFinanceOperationId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectIdFormatError);
        }
    }
}
