using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.Rentals
{
    public class RentalOrchestrationServiceException : NetXception
    {
        public RentalOrchestrationServiceException(Exception innerException)
            : base(message: "Rental service error occurred, contact support.", innerException) { }
    }
}
