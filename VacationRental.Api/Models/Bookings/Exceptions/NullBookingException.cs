using NetXceptions;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class NullBookingException : NetXception
    {
        public NullBookingException()
            : base(message: "Booking is null.")
        { }
    }
}
