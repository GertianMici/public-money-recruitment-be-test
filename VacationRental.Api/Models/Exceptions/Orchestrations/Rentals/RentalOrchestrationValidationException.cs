using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.Rentals
{
    public class RentalOrchestrationValidationException : NetXception
    {
        public RentalOrchestrationValidationException(NetXception innerException)
            : base(message: "Rental validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
