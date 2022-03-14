using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Brokers.Storages;
using VacationRental.Api.Models.Bookings;

namespace VacationRental.Api.Services.Foundations.Bookings
{
    public partial class BookingService : IBookingService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public BookingService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Booking> AddBookingAsync(Booking booking) =>
        TryCatch(async () =>
        {
            ValidateBookingOnAdd(booking);

            return await this.storageBroker.InsertBookingAsync(booking);
        });

        public ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId) =>
        TryCatch(async () =>
        {
            ValidateBookingId(bookingId);

            Booking maybeBooking = await this.storageBroker
                .SelectBookingByIdAsync(bookingId);

            ValidateStorageBooking(maybeBooking, bookingId);

            return maybeBooking;
        });

        public IQueryable<Booking> RetrieveAllBookings() =>
        TryCatch(() => this.storageBroker.SelectAllBookings());

        public ValueTask<Booking> ModifyBookingAsync(Booking booking) =>
        TryCatch(async () =>
        {
            ValidateBookingOnModify(booking);

            Booking maybeBooking =
                await this.storageBroker.SelectBookingByIdAsync(booking.Id);

            ValidateStorageBooking(maybeBooking, booking.Id);

            return await this.storageBroker.UpdateBookingAsync(booking);
        });

        public ValueTask<Booking> RemoveBookingByIdAsync(int bookingId) =>
        TryCatch(async () =>
        {
            ValidateBookingId(bookingId);

            Booking maybeBooking = await this.storageBroker
                .SelectBookingByIdAsync(bookingId);

            ValidateStorageBooking(maybeBooking, bookingId);

            return await this.storageBroker
                .DeleteBookingAsync(maybeBooking);
        });
    }
}
