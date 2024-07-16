using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseFinanceOperations.Commands.Delete
{
    public sealed class DeleteExpenceFinanceOperationCommandValidator : AbstractValidator<DeleteExpenceFinanceOperationCommand>
    {
        public DeleteExpenceFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
