using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class CalendarProcessingDependencyException : NetXception
    {
        public CalendarProcessingDependencyException(Exception innerException) :
            base(message: "Calendar dependency error occurred, contact support.", innerException)
        { }
    }
}
