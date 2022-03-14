using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Orchestration;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.Services.Orchestration;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IBookingRentalOrchestrationService bookingRentalOrchestrationService;

        public CalendarController(
            IBookingRentalOrchestrationService bookingRentalOrchestrationService)
        {
            this.bookingRentalOrchestrationService = bookingRentalOrchestrationService;
        }

        [HttpGet]
        public async ValueTask<ActionResult<Calendar>> Get(int rentalId, DateTime start, int nights)
        {
            try
            {
                Calendar calendar =
                    await this.bookingRentalOrchestrationService.GetCalendar(
                        rentalId,
                        startDate: start,
                        nights);

                return Ok(calendar);
            }
            catch (BookingRentalOrchestrationValidationException exception)
                when (exception.InnerException is NotFoundRentalException
                    || exception.InnerException is NotFoundBookingException)
            {
                return NotFound(exception.InnerException?.Message);
            }
            catch (BookingRentalOrchestrationValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (BookingRentalOrchestrationDependencyValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (BookingRentalOrchestrationServiceException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
        }
    }
}
