using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class CalendarProcessingServiceException : NetXception
    {
        public CalendarProcessingServiceException(Exception innerException)
            : base(message: "Calendar service error occurred, contact support.", innerException) { }
    }
}
