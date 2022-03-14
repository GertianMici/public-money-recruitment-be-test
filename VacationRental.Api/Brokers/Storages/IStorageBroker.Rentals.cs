using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;

namespace VacationRental.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Rental> InsertRentalAsync(Rental rental);
        IQueryable<Rental> SelectAllRentals();
        ValueTask<Rental> SelectRentalByIdAsync(int rentalId);
        ValueTask<Rental> UpdateRentalAsync(Rental rental);
        ValueTask<Rental> DeleteRentalAsync(Rental rental);
    }
}
