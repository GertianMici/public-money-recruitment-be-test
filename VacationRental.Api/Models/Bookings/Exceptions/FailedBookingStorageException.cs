using NetXceptions;
using System;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class FailedBookingStorageException : NetXception
    {
        public FailedBookingStorageException(Exception innerException)
            : base(message: "Failed booking storage error occurred, contact support.", innerException)
        { }
    }
}
