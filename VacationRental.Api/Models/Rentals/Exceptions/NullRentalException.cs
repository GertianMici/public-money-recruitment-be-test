using NetXceptions;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class NullRentalException : NetXception
    {
        public NullRentalException()
            : base(message: "Rental is null.")
        { }
    }
}
