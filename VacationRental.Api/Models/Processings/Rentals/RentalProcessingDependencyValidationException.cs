using NetXceptions;

namespace VacationRental.Api.Models.Processings.Rentals
{
    public class RentalProcessingDependencyValidationException : NetXception
    {
        public RentalProcessingDependencyValidationException(NetXception innerException)
            : base(message: "Rental dependency validation occurred, please try again.", innerException)
        { }
    }
}
