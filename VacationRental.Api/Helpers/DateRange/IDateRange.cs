using System;

namespace VacationRental.Api.Helpers.DateRange
{
    public interface IDateRange
    {
        DateTime Start { get; }
        DateTime End { get; }
        bool Includes(DateTime value);
        bool Includes(IDateRange range);
        bool IncludesEndDate(DateTime endDate);
        bool IncludesStartDate(DateTime startDate);
        bool IsIncludedInRange(DateTime startDate, DateTime endDate);
    }
}
