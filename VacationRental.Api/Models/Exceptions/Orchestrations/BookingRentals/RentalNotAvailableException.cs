using NetXceptions;

namespace VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals
{
    public class RentalNotAvailableException : NetXception
    {
        public RentalNotAvailableException(int rentalId)
            : base(message: $"There is no unit available for rental with id: {rentalId}.")
        { }
    }
}
