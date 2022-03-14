using System;

namespace VacationRental.Api.Helpers.DateRange
{
    public interface IDateRange
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool Includes(DateTime value);
        bool Includes(IDateRange range);
        bool IncludesEndDate(DateTime value);
        bool IncludesStartDate(DateTime value);
        bool IsIncludedInRange(DateTime startDate, DateTime endDate);
    }
}
