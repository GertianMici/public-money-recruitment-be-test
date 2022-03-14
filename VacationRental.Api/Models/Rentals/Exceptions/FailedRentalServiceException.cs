using NetXceptions;
using System;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class FailedRentalServiceException : NetXception
    {
        public FailedRentalServiceException(Exception innerException)
            : base(message: "Failed rental service occurred, please contact support", innerException)
        { }
    }
}
