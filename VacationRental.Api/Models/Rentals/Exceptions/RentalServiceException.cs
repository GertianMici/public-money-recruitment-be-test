using NetXceptions;
using System;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class RentalServiceException : NetXception
    {
        public RentalServiceException(Exception innerException)
            : base(message: "Rental service error occurred, contact support.", innerException) { }
    }
}
