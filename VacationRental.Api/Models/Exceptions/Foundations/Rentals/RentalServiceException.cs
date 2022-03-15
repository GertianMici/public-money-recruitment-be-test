using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Rentals
{
    public class RentalServiceException : NetXception
    {
        public RentalServiceException(Exception innerException)
            : base(message: "Rental service error occurred, contact support.", innerException) { }
    }
}
