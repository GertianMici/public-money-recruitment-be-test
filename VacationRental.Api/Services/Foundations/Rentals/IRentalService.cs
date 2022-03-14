using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;

namespace VacationRental.Api.Services.Foundations.Rentals
{
    public interface IRentalService
    {
        ValueTask<Rental> AddRentalAsync(Rental rental);
        ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId);
        IQueryable<Rental> RetrieveAllRentals();
        ValueTask<Rental> ModifyRentalAsync(Rental rental);
        ValueTask<Rental> RemoveRentalByIdAsync(int rentalId);
    }
}