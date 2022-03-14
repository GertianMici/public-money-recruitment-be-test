using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.Services.Orchestrations;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRentalOrchestrationService bookingRentalOrchestrationService;

        public BookingsController(
            IBookingRentalOrchestrationService bookingRentalOrchestrationService)
        {
            this.bookingRentalOrchestrationService = bookingRentalOrchestrationService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async ValueTask<ActionResult<Booking>> Get(int bookingId)
        {
            try
            {
                Booking booking = await this.bookingRentalOrchestrationService.RetrieveBookingByIdAsync(bookingId);

                return Ok(booking);
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

        [HttpPost]
        public async ValueTask<ActionResult<ResourceIdViewModel>> Post(BookingBindingModel model)
        {
            try
            {
                ResourceIdViewModel bookingId =
                    await this.bookingRentalOrchestrationService.CreateBookingRentalAsync(model);

                return Ok(bookingId);
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
