using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class FailedCalendarProcessingServiceException : NetXception
    {
        public FailedCalendarProcessingServiceException(Exception innerException)
            : base(message: "Failed rental service occurred, please contact support", innerException)
        { }
    }
}
