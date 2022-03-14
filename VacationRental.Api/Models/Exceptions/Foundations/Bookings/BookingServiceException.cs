using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class BookingServiceException : NetXception
    {
        public BookingServiceException(Exception innerException)
            : base(message: "Booking service error occurred, contact support.", innerException) { }
    }
}
