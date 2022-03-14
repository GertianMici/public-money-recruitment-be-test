using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.Services.Processings.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalProcessingService rentalProcessingService;

        public RentalsController(IRentalProcessingService rentalProcessingService) =>
            this.rentalProcessingService = rentalProcessingService;


        [HttpGet]
        [Route("{rentalId:int}")]
        public async ValueTask<ActionResult<Rental>> Get(int rentalId)
        {
            try
            {
                Rental rental = await this.rentalProcessingService.RetrieveRentalByIdAsync(rentalId);

                return Ok(rental);
            }
            catch (RentalProcessingValidationException rentalValidationException)
                when (rentalValidationException.InnerException is NotFoundRentalException)
            {
                return NotFound(rentalValidationException.InnerException?.Message);
            }
            catch (RentalProcessingValidationException rentalValidationException)
            {
                return BadRequest(rentalValidationException.InnerException?.Message);
            }
            catch (RentalProcessingDependencyException rentalDependencyException)
            {
                return BadRequest(rentalDependencyException.InnerException?.Message);
            }
            catch (RentalProcessingDependencyValidationException
                rentalDependencyValidationException)
            {
                return BadRequest(rentalDependencyValidationException.InnerException?.Message);
            }
            catch (RentalProcessingServiceException rentalServiceException)
            {
                return BadRequest(rentalServiceException.InnerException?.Message);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResourceIdViewModel>> Post(RentalBindingModel rentalModel)
        {
            try
            {
                ResourceIdViewModel rentalId =
                    await this.rentalProcessingService.AddRentalAsync(rentalModel);

                return Ok(rentalId);
            }
            catch (RentalProcessingValidationException rentalValidationException)
            {
                return BadRequest(rentalValidationException.InnerException?.Message);
            }
            catch (RentalProcessingDependencyValidationException rentalDependencyValidationException)
            {
                return Conflict(rentalDependencyValidationException.InnerException?.Message);
            }
            catch (RentalProcessingDependencyException rentalDependencyException)
            {
                return BadRequest(rentalDependencyException.InnerException?.Message);
            }
            catch (RentalProcessingServiceException rentalServiceException)
            {
                return BadRequest(rentalServiceException.InnerException?.Message);
            }
        }
    }
}
