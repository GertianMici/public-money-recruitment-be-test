using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Foundations.Rentals
{
    public class RentalValidationException : NetXception
    {
        public RentalValidationException(NetXception innerException)
            : base(message: "Rental validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
