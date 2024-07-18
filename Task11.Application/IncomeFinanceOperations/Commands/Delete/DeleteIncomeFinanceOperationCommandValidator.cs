using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeFinanceOperations.Commands.Delete
{
    public sealed class DeleteIncomeFinanceOperationCommandValidator : AbstractValidator<DeleteIncomeFinanceOperationCommand>
    {
        public DeleteIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.IncomeFinanceOperationId)
                .NotEmpty();
        }
    }
}
