using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Processings.Bookings
{
    public class BookingProcessingDependencyValidationException : NetXception
    {
        public BookingProcessingDependencyValidationException(NetXception innerException)
            : base(message: "Booking dependency validation occurred, please try again.", innerException)
        { }
    }
}
