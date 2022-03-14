using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Processings.Bookings
{
    public class BookingProcessingDependencyException : NetXception
    {
        public BookingProcessingDependencyException(Exception innerException) :
            base(message: "Booking dependency error occurred, contact support.", innerException)
        { }
    }
}
