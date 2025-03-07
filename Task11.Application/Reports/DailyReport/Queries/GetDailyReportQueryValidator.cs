﻿using FluentValidation;
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
