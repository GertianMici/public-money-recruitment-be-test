using NetXceptions;
using System;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class BookingServiceException : NetXception
    {
        public BookingServiceException(Exception innerException)
            : base(message: "Booking service error occurred, contact support.", innerException) { }
    }
}
