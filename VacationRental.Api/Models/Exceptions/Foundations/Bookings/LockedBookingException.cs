using NetXceptions;
using System;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class LockedBookingException : NetXception
    {
        public LockedBookingException(Exception innerException)
            : base(message: "Locked booking record exception, please try again later", innerException) { }
    }
}
