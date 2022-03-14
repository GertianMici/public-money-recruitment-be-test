using System;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestration
{
    public interface IBookingRentalOrchestrationService
    {
        ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId);
        ValueTask<ResourceIdViewModel> CreateBookingRentalAsync(BookingBindingModel bookingModel);
        ValueTask<Calendar> GetCalendar(int rentalId, DateTime startDate, int nights);
    }
}
