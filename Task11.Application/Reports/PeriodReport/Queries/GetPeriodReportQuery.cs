using ErrorOr;
using MediatR;

namespace Task11.Application.Reports.PeriodReport.Queries
{
    public record GetPeriodReportQuery(string StartDate, string EndDate) : IRequest<ErrorOr<PeriodReport>>;
}
