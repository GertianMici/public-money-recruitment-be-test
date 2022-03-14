using NetXceptions;

namespace VacationRental.Api.Models.Processings.Calendars
{
    public class CalendarProcessingDependencyValidationException : NetXception
    {
        public CalendarProcessingDependencyValidationException(NetXception innerException)
            : base(message: "Calendar dependency validation occurred, please try again.", innerException)
        { }
    }
}
