using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Processings.Rentals
{
    public class RentalProcessingValidationException : NetXception
    {
        public RentalProcessingValidationException(NetXception innerException)
            : base(message: "Rental validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
