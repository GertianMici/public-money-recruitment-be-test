using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Rentals
{
    public interface IRentalProcessingService
    {
        ValueTask<ResourceIdViewModel> AddRentalAsync(RentalBindingModel rentalModel);
        ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId);
        IQueryable<Rental> RetrieveAllRentals();
    }
}
