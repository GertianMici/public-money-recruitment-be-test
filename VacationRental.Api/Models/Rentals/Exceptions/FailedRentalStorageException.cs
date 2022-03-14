using NetXceptions;
using System;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class FailedRentalStorageException : NetXception
    {
        public FailedRentalStorageException(Exception innerException)
            : base(message: "Failed rental storage error occurred, contact support.", innerException)
        { }
    }
}
