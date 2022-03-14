using NetXceptions;
using System;

namespace VacationRental.Api.Models.Orchestration
{
    public class BookingRentalOrchestrationServiceException : NetXception
    {
        public BookingRentalOrchestrationServiceException(Exception innerException)
            : base(message: "Booking service error occurred, contact support.", innerException) { }
    }
}
