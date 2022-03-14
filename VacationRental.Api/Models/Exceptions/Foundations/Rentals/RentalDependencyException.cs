using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Rentals
{
    public class RentalDependencyException : NetXception
    {
        public RentalDependencyException(Exception innerException) :
            base(message: "Rental dependency error occurred, contact support.", innerException)
        { }
    }
}
