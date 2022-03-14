using NetXceptions;
using System;

namespace VacationRental.Api.Models.Processings.Rentals
{
    public class RentalProcessingServiceException : NetXception
    {
        public RentalProcessingServiceException(Exception innerException)
            : base(message: "Rental service error occurred, contact support.", innerException) { }
    }
}
