using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationRental.Api.Models.Exceptions.Orchestrations.Rentals;
using VacationRental.Api.Models.Exceptions.Processings.Rentals;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.Services.Orchestrations.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalOrchestrationService rentalOrchestrationService;

        public RentalsController(IRentalOrchestrationService rentalOrchestrationService) =>
            this.rentalOrchestrationService = rentalOrchestrationService;


        [HttpGet]
        [Route("{rentalId:int}")]
        public async ValueTask<ActionResult<Rental>> Get(int rentalId)
        {
            try
            {
                Rental rental = 
                    await this.rentalOrchestrationService.RetrieveRentalByIdAsync(rentalId);

                return Ok(rental);
            }
            catch (RentalOrchestrationValidationException exception)
                when (exception.InnerException is NotFoundRentalException)
            {
                return NotFound(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationDependencyValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationServiceException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
        }

        [HttpPost]
        public async ValueTask<ActionResult<ResourceIdViewModel>> Post(RentalBindingModel rentalModel)
        {
            try
            {
                ResourceIdViewModel rentalId =
                    await this.rentalOrchestrationService.AddRentalAsync(rentalModel);

                return Ok(rentalId);
            }
            catch (RentalOrchestrationValidationException exception)
                when (exception.InnerException is NotFoundRentalException)
            {
                return NotFound(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationDependencyValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationServiceException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public async ValueTask<ActionResult<ResourceIdViewModel>> Put(int rentalId, RentalBindingModel rentalModel)
        {
            try
            {
                Rental rental =
                    await this.rentalOrchestrationService.ModifyRentalAsync(rentalId, rentalModel);

                return Ok(rental);
            }
            catch (RentalOrchestrationValidationException exception)
                when (exception.InnerException is NotFoundRentalException)
            {
                return NotFound(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationDependencyValidationException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
            catch (RentalOrchestrationServiceException exception)
            {
                return BadRequest(exception.InnerException?.Message);
            }
        }
    }
}
