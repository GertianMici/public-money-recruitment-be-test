using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Bookings
{
    public class BookingProcessingDependencyException : NetXception
    {
        public BookingProcessingDependencyException(Exception innerException) :
            base(message: "Booking dependency error occurred, contact support.", innerException)
        { }
    }
}
