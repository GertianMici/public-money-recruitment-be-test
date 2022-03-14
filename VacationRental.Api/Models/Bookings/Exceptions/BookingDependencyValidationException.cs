using NetXceptions;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class BookingDependencyValidationException : NetXception
    {
        public BookingDependencyValidationException(NetXception innerException)
        : base(message: "Booking dependency validation occurred, please try again.", innerException)
        { }
    }
}
