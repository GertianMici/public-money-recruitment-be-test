using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Rentals
{
    public interface IRentalOrchestrationService
    {
        ValueTask<ResourceIdViewModel> AddRentalAsync(RentalBindingModel rentalModel);
        ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId);
        ValueTask<Rental> ModifyRentalAsync(int rentalId, RentalBindingModel rentalModel);
    }
}
