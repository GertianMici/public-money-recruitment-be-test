using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class BookingDependencyException : NetXception
    {
        public BookingDependencyException(Exception innerException) :
            base(message: "Booking dependency error occurred, contact support.", innerException)
        { }
    }
}
