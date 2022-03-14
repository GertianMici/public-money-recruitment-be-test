using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Processings.Rentals
{
    public class RentalProcessingDependencyException : NetXception
    {
        public RentalProcessingDependencyException(Exception innerException) :
            base(message: "Rental dependency error occurred, contact support.", innerException)
        { }
    }
}
