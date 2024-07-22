using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Properties;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public sealed class GetDailyReportQueryValidator : AbstractValidator<GetDailyReportQuery>
    {
        public GetDailyReportQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);
        }
    }
}
