using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class FailedBookingStorageException : NetXception
    {
        public FailedBookingStorageException(Exception innerException)
            : base(message: "Failed booking storage error occurred, contact support.", innerException)
        { }
    }
}
