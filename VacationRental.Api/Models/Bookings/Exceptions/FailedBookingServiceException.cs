using NetXceptions;
using System;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class FailedBookingServiceException : NetXception
    {
        public FailedBookingServiceException(Exception innerException)
            : base(message: "Failed booking service occurred, please contact support", innerException)
        { }
    }
}
