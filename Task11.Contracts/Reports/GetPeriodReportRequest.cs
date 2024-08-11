using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Contracts.Reports
{
    public record GetPeriodReportRequest(string StartDate, string EndDate);
}
