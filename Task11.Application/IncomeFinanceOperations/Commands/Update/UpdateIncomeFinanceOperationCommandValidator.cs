using FluentValidation;
using Task11.Domain.Common.Сonstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.IncomeFinanceOperations.Commands.Update;

namespace Task11.Application.IncomeFinanceOperations.Commands.Update
{
    public sealed class UpdateIncomeFinanceOperationCommandValidator : AbstractValidator<UpdateIncomeFinanceOperationCommand>
    {
        public UpdateIncomeFinanceOperationCommandValidator()
        {
            RuleFor(x => x.IncomeFinanceOperationId)
                .NotEmpty();

            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.ExpenseFinanceOperation.DateFormat, out _))
                .WithMessage("Incorrect date format, coorect format is yyyy-MM-DD");

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
