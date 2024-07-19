using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.Reports.DailyReport.Queries
{
    public record GetDailyReportQuery(DateOnly Date) : IRequest<ErrorOr<DailyReport>>;
}
