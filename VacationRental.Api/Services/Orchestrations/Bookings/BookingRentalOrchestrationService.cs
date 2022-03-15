using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Helpers.DateRange;
using VacationRental.Api.Models;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Services.Orchestrations.Bookings;
using VacationRental.Api.Services.Processings.Bookings;
using VacationRental.Api.Services.Processings.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Bookings
{
    public partial class BookingRentalOrchestrationService : IBookingRentalOrchestrationService
    {
        private readonly IBookingProcessingService bookingProcessingService;
        private readonly IRentalProcessingService rentalProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public BookingRentalOrchestrationService(
            IBookingProcessingService bookingProcessingService,
            IRentalProcessingService rentalProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.bookingProcessingService = bookingProcessingService;
            this.rentalProcessingService = rentalProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ResourceIdViewModel> CreateBookingRentalAsync(BookingBindingModel bookingModel) =>
            TryCatch(async () =>
            {
                ValidateBookingOnAdd(bookingModel);

                Rental storageRental =
                    await this.rentalProcessingService.RetrieveRentalByIdAsync(bookingModel.RentalId);

                ValidateRentalIsNull(storageRental);

                IQueryable<Booking> allBookingsForGivenRental =
                    this.bookingProcessingService.RetrieveAllBookings()
                        .Where(booking => booking.RentalId == bookingModel.RentalId);


                var unitsBookedList = new List<int>();

                var newBookingRange = new DateRange(
                    bookingModel.Start,
                    bookingModel.Start.AddDays(bookingModel.Nights + storageRental.PreparationTimeInDays));

                foreach (Booking booking in allBookingsForGivenRental)
                {
                    if (HasExistingBookingInDateRange(newBookingRange, booking, storageRental.PreparationTimeInDays))
                    {
                        unitsBookedList.Add(booking.Unit);
                    }
                }

                int? newBookingUnit = 0;

                if (ValidateUnitsAvailability(unitsBookedList.Count, storageRental))
                {
                    newBookingUnit = Enumerable.Range(1, storageRental.Units).Except(unitsBookedList)?.First();
                };

                return await this.bookingProcessingService.AddBookingAsync(bookingModel, newBookingUnit);
            });

        public ValueTask<Calendar> GetCalendar(int rentalId, DateTime startDate, int nights) =>
            TryCatch(async () =>
            {
                ValidateRentalId(rentalId);
                ValidateNightsArePositive(nights);

                Rental storageRental =
                    await this.rentalProcessingService.RetrieveRentalByIdAsync(rentalId);

                ValidateRentalIsNull(storageRental);

                var calendar = new Calendar
                {
                    RentalId = rentalId,
                    Dates = new List<CalendarDateBooking>()
                };

                IQueryable<Booking> allBookingsForGivenRental =
                   this.bookingProcessingService.RetrieveAllBookings()
                       .Where(booking => booking.RentalId == rentalId);

                for (var i = 0; i < nights; i++)
                {
                    var date = new CalendarDateBooking
                    {
                        Date = startDate.Date.AddDays(i),
                        Bookings = new List<CalendarBookingViewModel>(),
                        PreparationTimes = new List<PreparationTime>()
                    };

                    foreach (var booking in allBookingsForGivenRental)
                    {
                        if (booking.Start <= date.Date
                            && booking.Start.AddDays(booking.Nights) > date.Date)
                        {
                            date.Bookings.Add(
                                new CalendarBookingViewModel
                                {
                                    Id = booking.Id,
                                    Unit = booking.Unit
                                });

                        }
                        if (date.Date >= booking.Start.AddDays(booking.Nights) &&
                            date.Date < booking.Start.AddDays(booking.Nights + storageRental.PreparationTimeInDays))
                        {
                            date.PreparationTimes.Add(
                                 new PreparationTime
                                 {
                                     Unit = booking.Unit
                                 });
                        }
                    }

                    calendar.Dates.Add(date);
                }

                return calendar;
            });

        public ValueTask<Booking> RetrieveBookingByIdAsync(int bookingId) =>
            TryCatch(async () =>
            {
                ValidateBookingId(bookingId);

                Booking booking =
                    await this.bookingProcessingService.RetrieveBookingByIdAsync(bookingId);

                ValidateStorageBooking(booking, bookingId);

                return booking;
            });
    }
}
