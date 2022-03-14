using NetXceptions;

namespace VacationRental.Api.Models.Calendars.Exceptons
{
    public class NotFoundCalendarException : NetXception
    {
        public NotFoundCalendarException(int rentalId)
           : base(message: $"Couldn't find calendar for retalId: {rentalId}.")
        { }
    }
}
