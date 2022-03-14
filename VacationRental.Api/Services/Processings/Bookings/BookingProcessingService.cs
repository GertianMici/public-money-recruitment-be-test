using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Services.Foundations.Bookings;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Bookings
{
    public partial class BookingProcessingService : IBookingProcessingService
    {
        private readonly IBookingService bookingService;
        private readonly ILoggingBroker loggingBroker;
        public BookingProcessingService(
            IBookingService bookingService,
            ILoggingBroker loggingBroker)
        {
            this.bookingService = bookingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ResourceIdViewModel> AddBookingAsync(BookingBindingModel bookingModel, int? unit) =>
            TryCatch(async () =>
            {
                ValidateBookingOnAdd(bookingModel);

                var booking = new Booking
                {
                    RentalId = bookingModel.RentalId,
                    Nights = bookingModel.Nights,
                    Start = bookingModel.Start,
                    Unit = unit ?? 0
                };

                Booking storageBooking =
                    await this.bookingService.AddBookingAsync(booking);

                return new ResourceIdViewModel { Id = storageBooking.Id };
            });

        public ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId) =>
            TryCatch(async () =>
            {
                ValidateBookingId(bookingId);

                Booking maybeBooking =
                    await this.bookingService.RetrieveBookingByIdAsync(bookingId);

                ValidateStorageBooking(maybeBooking, bookingId);

                return maybeBooking;
            });

        public IQueryable<Booking> RetrieveAllBookings() =>
            TryCatch(() => this.bookingService.RetrieveAllBookings());
    }
}
