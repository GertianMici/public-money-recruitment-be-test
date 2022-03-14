using NetXceptions;

namespace VacationRental.Api.Models.Processings.Bookings
{
    public class BookingProcessingDependencyValidationException : NetXception
    {
        public BookingProcessingDependencyValidationException(NetXception innerException)
            : base(message: "Booking dependency validation occurred, please try again.", innerException)
        { }
    }
}
