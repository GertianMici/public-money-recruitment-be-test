using System.Collections.Generic;

namespace VacationRental.Api.Models.Calendars
{
    public class Calendar
    {
        public int RentalId { get; set; }
        public List<CalendarDateBooking> Dates { get; set; }
    }
}
