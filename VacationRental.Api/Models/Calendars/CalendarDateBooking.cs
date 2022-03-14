using System;
using System.Collections.Generic;

namespace VacationRental.Api.Models.Calendars
{
    public class CalendarDateBooking
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
    }
}
