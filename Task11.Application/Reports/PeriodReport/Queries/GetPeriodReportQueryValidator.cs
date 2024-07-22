using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task11.Application.Properties;

namespace Task11.Application.Reports.PeriodReport.Queries
{
    public sealed class GetPeriodReportQueryValidator : AbstractValidator<GetPeriodReportQuery>
    {
        public GetPeriodReportQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .Must(x => DateTime.TryParse(x, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);
        }
    }
}
