namespace Task11.Application.Reports.PeriodReport
{
    public readonly struct DatePeriod : IEquatable<DatePeriod>
    {
        public DatePeriod(DateOnly startDate, DateOnly endDate)
        {
            ThrowIfStartDateGreaterThanEndDate(startDate, endDate);

            StartDate = startDate;
            EndDate = endDate;
        }

        public DatePeriod(string startDate, string endDate)
        {
            DateOnly start = DateOnly.Parse(startDate);
            DateOnly end = DateOnly.Parse(endDate);

            ThrowIfStartDateGreaterThanEndDate(start, end);

            StartDate = start;
            EndDate = end;
        }

        public readonly DateOnly StartDate { get; }
        public readonly DateOnly EndDate { get; }

        public bool Contains(DateOnly date)
        {
            return date >= StartDate && date <= EndDate;
        }

        public static bool operator ==(DatePeriod left, DatePeriod right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DatePeriod left, DatePeriod right)
        {
            return !(left == right);
        }

        private void ThrowIfStartDateGreaterThanEndDate(DateOnly startDate, DateOnly endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be greater than end date.");
            }
        }

        public bool Equals(DatePeriod other)
        {
            return StartDate == other.StartDate && EndDate == other.EndDate;
        }
    }
}
