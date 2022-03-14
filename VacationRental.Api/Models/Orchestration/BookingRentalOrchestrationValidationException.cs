using NetXceptions;

namespace VacationRental.Api.Models.Orchestration
{
    public class BookingRentalOrchestrationValidationException : NetXception
    {
        public BookingRentalOrchestrationValidationException(NetXception innerException)
            : base(message: "Booking or rental validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
