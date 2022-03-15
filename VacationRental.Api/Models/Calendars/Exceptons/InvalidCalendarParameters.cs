using NetXceptions;

namespace VacationRental.Api.Models.Calendars.Exceptons
{
    public class InvalidCalendarParameters : NetXception
    {
        public InvalidCalendarParameters(string message) : base(message)
        {
        }
    }
}
