using ErrorOr;
using MediatR;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public record GetDailyReportQuery(string Date) : IRequest<ErrorOr<DailyReport>>;
}
