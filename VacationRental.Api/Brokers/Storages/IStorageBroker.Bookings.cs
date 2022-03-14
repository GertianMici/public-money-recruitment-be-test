using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;

namespace VacationRental.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Booking> InsertBookingAsync(Booking booking);
        IQueryable<Booking> SelectAllBookings();
        ValueTask<Booking> SelectBookingByIdAsync(int bookingId);
        ValueTask<Booking> UpdateBookingAsync(Booking booking);
        ValueTask<Booking> DeleteBookingAsync(Booking booking);
    }
}
