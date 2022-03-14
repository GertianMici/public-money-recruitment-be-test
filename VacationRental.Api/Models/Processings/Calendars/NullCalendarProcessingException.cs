using NetXceptions;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class NullCalendarProcessingException : NetXception
    {
        public NullCalendarProcessingException()
            : base(message: "Calendar is null.")
        { }
    }
}
