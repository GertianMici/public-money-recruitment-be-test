using NetXceptions;

namespace VacationRental.Api.Models.Orchestration
{
    public class RentalNotAvailableException : NetXception
    {
        public RentalNotAvailableException(int rentalId) 
            : base(message: $"There is no unit available for rental with id: {rentalId}.")
        { }
    }
}
