﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Calendars;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Services.Processings.Bookings;
using VacationRental.Api.Services.Processings.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestration
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

                IQueryable<Booking> allBookings =
                    this.bookingProcessingService.RetrieveAllBookings();

                int unitsBooked = 0;

                foreach (Booking booking in allBookings)
                {
                    if (ValidateBookingAvailability(bookingModel, booking))
                        unitsBooked++;
                }

                ValidateUnitsAvailability(unitsBooked, storageRental);

                return await this.bookingProcessingService.AddBookingAsync(bookingModel);
            });

        public ValueTask<Calendar> GetCalendar(int rentalId, DateTime startDate, int nights) =>
            TryCatch(async () =>
            {
                ValidateNightsArePositive(nights);

                Rental storageRental =
                    await this.rentalProcessingService.RetrieveRentalByIdAsync(rentalId);

                ValidateRentalIsNull(storageRental);

                var calendar = new Calendar
                {
                    RentalId = rentalId,
                    Dates = new List<CalendarDateBooking>()
                };

                IQueryable<Booking> allBookings =
                    this.bookingProcessingService.RetrieveAllBookings();


                for (var i = 0; i < nights; i++)
                {
                    var date = new CalendarDateBooking
                    {
                        Date = startDate.Date.AddDays(i),
                        Bookings = new List<CalendarBookingViewModel>()
                    };

                    foreach (var booking in allBookings)
                    {
                        if (booking.RentalId == rentalId
                            && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                        {
                            date.Bookings.Add(
                                new CalendarBookingViewModel
                                {
                                    Id = booking.Id
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
