using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Rentals;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IDictionary<int, Rental> _rentals;
        private readonly IDictionary<int, Booking> _bookings;

        public CalendarController(
            IDictionary<int, Rental> rentals,
            IDictionary<int, Booking> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        [HttpGet]
        public Calendar Get(int rentalId, DateTime start, int nights)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var result = new Calendar
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateBooking>()
            };
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateBooking
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<int>()
                };

                foreach (var booking in _bookings.Values)
                {
                    if (booking.RentalId == rentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(booking.Id);
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}
