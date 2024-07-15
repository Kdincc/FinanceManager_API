using FluentValidation;
using Task11.Domain.Common.Сonstants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.ExpenseTypes.Commands.Update
{
    public sealed class UpdateExpenseTypeCommandValidator : AbstractValidator<UpdateExpenseTypeCommand>
    {
        public UpdateExpenseTypeCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxNameLength);

            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstantst.OperationType.MaxDescriptionLength);

            RuleFor(p => p.ExpenseTypeId)
                .NotEmpty();
        }
    }
}
