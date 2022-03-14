using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Bookings
{
    public interface IBookingProcessingService
    {
        ValueTask<ResourceIdViewModel> AddBookingAsync(BookingBindingModel bookingModel);
        ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId);
        IQueryable<Booking> RetrieveAllBookings();
    }
}
