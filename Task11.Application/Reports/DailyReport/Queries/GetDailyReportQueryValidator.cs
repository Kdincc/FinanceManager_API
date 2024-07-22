using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Properties;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public sealed class GetDailyReportQueryValidator : AbstractValidator<GetDailyReportQuery>
    {
        public GetDailyReportQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.FinanceOperation.DateFormat, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);
        }
    }
}
