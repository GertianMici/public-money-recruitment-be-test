using NetXceptions;

namespace VacationRental.Api.Models.Calendars.Exceptons
{
    public class NullCalendarException : NetXception
    {
        public NullCalendarException()
            : base(message: "Calendar is null.")
        { }
    }
}
