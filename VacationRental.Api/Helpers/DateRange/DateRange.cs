using System;

namespace VacationRental.Api.Helpers.DateRange
{
    public class DateRange : IDateRange
    {
        public DateRange(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public bool Includes(DateTime value)
        {
            return Start <= value && value <= End;
        }

        public bool Includes(IDateRange range)
        {
            return Start <= range.Start && range.End <= End;
        }

        /// <summary>
        /// Check if given booking start date is included in new booking date range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IncludesStartDate(DateTime value)
        {
            return Start <= value && value < End;
        }

        /// <summary>
        /// check if given booking end date is included in new booking date range
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IncludesEndDate(DateTime value)
        {
            return Start < value && value <= End;
        }

        /// <summary>
        /// check if new new booking date range is included in an existing booking
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public bool IsIncludedInRange(DateTime startDate, DateTime endDate)
        {
            return startDate < Start && endDate > End;
        }
    }
}
