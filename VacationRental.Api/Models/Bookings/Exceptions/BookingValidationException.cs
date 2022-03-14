using NetXceptions;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class BookingValidationException : NetXception
    {
        public BookingValidationException(NetXception innerException)
          : base(message: "Booking validation errors occurred, please try again.",
                innerException)
        { }
    }
}
