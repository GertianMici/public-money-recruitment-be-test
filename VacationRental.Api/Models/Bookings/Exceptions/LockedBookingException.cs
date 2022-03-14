using NetXceptions;
using System;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class LockedBookingException : NetXception
    {
        public LockedBookingException(Exception innerException)
            : base(message: "Locked booking record exception, please try again later", innerException) { }
    }
}
