using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals
{
    public class BookingRentalOrchestrationDependencyValidationException : NetXception
    {
        public BookingRentalOrchestrationDependencyValidationException(NetXception innerException)
            : base(message: "Booking dependency validation occurred, please try again.", innerException)
        { }
    }
}
