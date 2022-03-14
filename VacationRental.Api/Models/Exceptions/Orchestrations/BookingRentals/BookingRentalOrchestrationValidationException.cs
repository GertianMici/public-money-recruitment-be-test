using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals
{
    public class BookingRentalOrchestrationValidationException : NetXception
    {
        public BookingRentalOrchestrationValidationException(NetXception innerException)
            : base(message: "Booking or rental validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
