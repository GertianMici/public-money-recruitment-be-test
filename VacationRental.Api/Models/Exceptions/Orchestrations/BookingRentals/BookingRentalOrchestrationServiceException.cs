using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals
{
    public class BookingRentalOrchestrationServiceException : NetXception
    {
        public BookingRentalOrchestrationServiceException(Exception innerException)
            : base(message: "Booking service error occurred, contact support.", innerException) { }
    }
}
