using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.Reports.PeriodReport.Queries
{
    public record GetPeriodReportQuery(string StartDate, string EndDate) : IRequest<ErrorOr<PeriodReport>>;
}
