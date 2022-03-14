using NetXceptions;
using System;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class BookingDependencyException : NetXception
    {
        public BookingDependencyException(Exception innerException) :
            base(message: "Booking dependency error occurred, contact support.", innerException)
        { }
    }
}
