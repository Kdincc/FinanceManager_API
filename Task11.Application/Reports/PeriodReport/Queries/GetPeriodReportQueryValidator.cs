using FluentValidation;
using Task11.Application.Properties;
using Task11.Domain.Common.Сonstants;

namespace Task11.Application.Reports.PeriodReport.Queries
{
    public sealed class GetPeriodReportQueryValidator : AbstractValidator<GetPeriodReportQuery>
    {
        public GetPeriodReportQueryValidator()
        {
            RuleFor(x => x.StartDate)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.FinanceOperation.DateFormat, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .Must(x => DateOnly.TryParseExact(x, ValidationConstants.FinanceOperation.DateFormat, out _))
                .WithMessage(ValidationErrorMessages.IncorrectDateFormat);
        }
    }
}
