using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;

namespace VacationRental.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Rental> Rentals { get; set; }

        public async ValueTask<Rental> InsertRentalAsync(Rental rental)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Rental> rentalEntityEntry =
                await broker.Rentals.AddAsync(rental);

            await broker.SaveChangesAsync();

            return rentalEntityEntry.Entity;
        }

        public IQueryable<Rental> SelectAllRentals()
        {
            using var broker =
                new StorageBroker(this.configuration);

            return broker.Rentals;
        }

        public async ValueTask<Rental> SelectRentalByIdAsync(int rentalId)
        {
            using var broker =
                new StorageBroker(this.configuration);

            return await broker.Rentals.FindAsync(rentalId);
        }

        public async ValueTask<Rental> UpdateRentalAsync(Rental rental)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Rental> rentalEntityEntry =
                broker.Rentals.Update(rental);

            await broker.SaveChangesAsync();

            return rentalEntityEntry.Entity;
        }

        public async ValueTask<Rental> DeleteRentalAsync(Rental rental)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Rental> rentalEntityEntry =
                broker.Rentals.Remove(rental);

            await broker.SaveChangesAsync();

            return rentalEntityEntry.Entity;
        }
    }
}
