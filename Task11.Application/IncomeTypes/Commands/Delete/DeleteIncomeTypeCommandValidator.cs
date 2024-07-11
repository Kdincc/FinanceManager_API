using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeTypes.Commands.Delete
{
    public sealed class DeleteIncomeTypeCommandValidator : AbstractValidator<DeleteIncomeTypeCommand>
    {
        public DeleteIncomeTypeCommandValidator()
        {
            RuleFor(i => i.IncomeTypeId).NotEmpty();
        }
    }
}
