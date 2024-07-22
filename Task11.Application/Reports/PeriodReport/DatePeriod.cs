using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task11.Application.Reports.PeriodReport
{
    public readonly struct DatePeriod
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

        public DatePeriod(string startDate, string endDate, string format)
        {
            DateOnly start = DateOnly.ParseExact(startDate, format);
            DateOnly end = DateOnly.ParseExact(endDate, format);

            if (start > end)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }

            StartDate = start;
            EndDate = end;
        }

        public readonly DateOnly StartDate { get; }
        public readonly DateOnly EndDate { get; }

        public bool Contains(DateOnly date)
        {
            return date >= StartDate && date <= EndDate;
        }

    }
}
