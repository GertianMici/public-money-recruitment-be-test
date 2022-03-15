using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class FailedBookingServiceException : NetXception
    {
        public FailedBookingServiceException(Exception innerException)
            : base(message: "Failed booking service occurred, please contact support", innerException)
        { }
    }
}
