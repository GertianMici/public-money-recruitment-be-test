using NetXceptions;

namespace VacationRental.Api.Models.Bookings.Exceptions
{
    public class NotFoundBookingException : NetXception
    {
        public NotFoundBookingException(int bookingId)
            : base(message: $"Couldn't find booking with id: {bookingId}.")
        { }
    }
}
