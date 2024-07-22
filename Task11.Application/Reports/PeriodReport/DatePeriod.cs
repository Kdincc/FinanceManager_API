using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.Reports.PeriodReport
{
    public struct DatePeriod
    {
        public DatePeriod(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }

            StartDate = startDate;
            EndDate = endDate;
        }

        public DateOnly StartDate { get; }
        public DateOnly EndDate { get; }
    }
}
