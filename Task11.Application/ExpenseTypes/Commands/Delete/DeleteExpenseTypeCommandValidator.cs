using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseTypes.Commands.Delete
{
    public sealed class DeleteExpenseTypeCommandValidator : AbstractValidator<DeleteExpenseTypeCommand>
    {
        public DeleteExpenseTypeCommandValidator()
        {
            RuleFor(p => p.ExpenseTypeId).NotEmpty();
        }
    }
}
