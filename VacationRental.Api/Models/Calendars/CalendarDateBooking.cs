using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models.Calendars
{
    public class CalendarDateBooking
    {
        public DateTime Date { get; set; }
        public List<int> Bookings { get; set; }
    }
}
