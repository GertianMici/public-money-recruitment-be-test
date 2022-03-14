using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Rentals
{
    public class FailedRentalStorageException : NetXception
    {
        public FailedRentalStorageException(Exception innerException)
            : base(message: "Failed rental storage error occurred, contact support.", innerException)
        { }
    }
}
