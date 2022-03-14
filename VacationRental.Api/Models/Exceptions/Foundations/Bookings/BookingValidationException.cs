using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Foundations.Bookings
{
    public class BookingValidationException : NetXception
    {
        public BookingValidationException(NetXception innerException)
          : base(message: "Booking validation errors occurred, please try again.",
                innerException)
        { }
    }
}
