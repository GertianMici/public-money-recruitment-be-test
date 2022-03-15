using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Processings.Rentals
{
    public class FailedRentalProcessingServiceException : NetXception
    {
        public FailedRentalProcessingServiceException(Exception innerException)
            : base(message: "Failed rental service occurred, please contact support", innerException)
        { }
    }
}
