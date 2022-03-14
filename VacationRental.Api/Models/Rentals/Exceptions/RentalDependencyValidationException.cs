using NetXceptions;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class RentalDependencyValidationException : NetXception
    {
        public RentalDependencyValidationException(NetXception innerException)
            : base(message: "Rental dependency validation occurred, please try again.", innerException)
        { }
    }
}
