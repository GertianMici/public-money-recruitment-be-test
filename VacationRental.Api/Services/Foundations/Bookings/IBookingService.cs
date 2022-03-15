using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;

namespace VacationRental.Api.Services.Foundations.Bookings
{
    public interface IBookingService
    {
        ValueTask<Booking> AddBookingAsync(Booking booking);
        ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId);
        IQueryable<Booking> RetrieveAllBookings();
        ValueTask<Booking> ModifyBookingAsync(Booking booking);
        ValueTask<Booking> RemoveBookingByIdAsync(int bookingId);
    }
}
