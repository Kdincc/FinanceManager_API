using FluentValidation;
using Task11.Domain.Common.Сonstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.IncomeFinanceOperations.Commands.Create
{
    public sealed class UpdateIncomeFinanceOperationCommandValidator : AbstractValidator<CreateIncomeFinanceOperationCommand>
    {
        public UpdateIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.ExpenseFinanceOperation.DateFormat, out _))
                .WithMessage("Incorrect date format, coorect format is yyyy-MM-DD");

            RuleFor(x => x.IncomeTypeId)
                .NotEmpty()
                .Must(x => Guid.TryParse(x, out _));

            RuleFor(x => x.Amount)
                .Must(x => x >= 0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(ValidationConstants.IncomeFinanceOperation.MaxNameLength);
        }
    }
}
