using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;

namespace VacationRental.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Booking> Bookings { get; set; }

        public async ValueTask<Booking> InsertBookingAsync(Booking booking)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Booking> bookingEntityEntry =
                await broker.Bookings.AddAsync(booking);

            await broker.SaveChangesAsync();

            return bookingEntityEntry.Entity;
        }

        public IQueryable<Booking> SelectAllBookings()
        {
            using var broker =
    new StorageBroker(this.configuration);

            return broker.Bookings;
        }

        public async ValueTask<Booking> SelectBookingByIdAsync(int bookingId)
        {
            using var broker =
                 new StorageBroker(this.configuration);

            return await broker.Bookings.FindAsync(bookingId);
        }

        public async ValueTask<Booking> UpdateBookingAsync(Booking booking)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Booking> bookingEntityEntry =
                broker.Bookings.Update(booking);

            await broker.SaveChangesAsync();

            return bookingEntityEntry.Entity;
        }

        public async ValueTask<Booking> DeleteBookingAsync(Booking booking)
        {
            using var broker =
                new StorageBroker(this.configuration);

            EntityEntry<Booking> bookingEntityEntry =
                broker.Bookings.Remove(booking);

            await broker.SaveChangesAsync();

            return bookingEntityEntry.Entity;
        }
    }
}
