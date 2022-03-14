using NetXceptions;

namespace VacationRental.Api.Models.Orchestration
{
    public class BookingRentalOrchestrationDependencyValidationException : NetXception
    {
        public BookingRentalOrchestrationDependencyValidationException(NetXception innerException)
            : base(message: "Booking dependency validation occurred, please try again.", innerException)
        { }
    }
}
