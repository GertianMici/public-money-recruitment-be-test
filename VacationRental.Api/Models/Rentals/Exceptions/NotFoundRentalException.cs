using NetXceptions;

namespace VacationRental.Api.Models.Rentals.Exceptions
{
    public class NotFoundRentalException : NetXception
    {
        public NotFoundRentalException()
            : base(message: "Couldn't find rentals.")
        {
        }

        public NotFoundRentalException(int rentalId)
           : base(message: $"Couldn't find rental with id: {rentalId}.")
        { }
    }
}
