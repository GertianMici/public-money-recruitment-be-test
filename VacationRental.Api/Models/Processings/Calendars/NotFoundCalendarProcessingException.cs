using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class NotFoundCalendarProcessingException : NetXception
    {
        public NotFoundCalendarProcessingException(int rentalId, DateTime start, int nights)
           : base(message: $"Couldn't find calendar with " +
                 $"id: {rentalId}," +
                 $"and start date: {start}, " +
                 $"and nights: {nights}")
        { }
    }
}
