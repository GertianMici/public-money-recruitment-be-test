using NetXceptions;
using System;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class RentalDependencyException : NetXception
    {
        public RentalDependencyException(Exception innerException) :
            base(message: "Rental dependency error occurred, contact support.", innerException)
        { }
    }
}
