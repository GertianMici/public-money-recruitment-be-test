using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.Rentals
{
    public class RentalOrchestrationDependencyValidationException : NetXception
    {
        public RentalOrchestrationDependencyValidationException(NetXception innerException)
            : base(message: "Rental dependency validation occurred, please try again.", innerException)
        { }
    }
}
