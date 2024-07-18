using FluentValidation;
using Task11.Domain.Common.Сonstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public sealed class CreateIncomeFinanceOperationCommandValidator : AbstractValidator<CreateIncomeFinanceOperationCommand>
    {
        public CreateIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty();

            RuleFor(x => x.IncomeTypeId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .Must(x => x.Value >= 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.IncomeFinanceOperation.MaxNameLength);
        }
    }
}
