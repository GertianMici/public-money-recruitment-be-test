using NetXceptions;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class CalendarProcessingValidationException : NetXception
    {
        public CalendarProcessingValidationException(NetXception innerException)
            : base(message: "Calendar validation errors occurred, please try again.",
                  innerException)
        { }
    }
}
