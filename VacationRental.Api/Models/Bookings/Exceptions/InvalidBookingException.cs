using NetXceptions;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class InvalidBookingException : NetXception
    {
        public InvalidBookingException()
            : base(message: "Invalid booking. Please correct the errors and try again.")
        { }
    }
}
