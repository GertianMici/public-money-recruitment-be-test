using System;
using System.Linq;
using VacationRental.Api.Helpers.DateRange;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.Models.Exceptions.Orchestrations.BookingRentals;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations
{
    public partial class BookingRentalOrchestrationService
    {
        private static void ValidateBookingOnAdd(BookingBindingModel bookingModel)
        {
            ValidateBookingIsNotNull(bookingModel);

            Validate(
                (Rule: IsInvalid(bookingModel.RentalId), Parameter: nameof(Booking.RentalId)),
                (Rule: IsInvalid(bookingModel.Start), Parameter: nameof(Booking.Start)),
                (Rule: IsInvalid(bookingModel.Nights), Parameter: nameof(Booking.Nights)));
        }

        private static void ValidateBookingOnModify(BookingBindingModel booking)
        {
            ValidateBookingIsNotNull(booking);

            Validate(
                (Rule: IsInvalid(booking.Nights), Parameter: nameof(booking.Nights)),
                (Rule: IsInvalid(booking.RentalId), Parameter: nameof(booking.RentalId))
                );
        }

        private void ValidateBookingId(int bookingId) =>
           Validate((Rule: IsInvalid(bookingId), Parameter: nameof(Booking.Id)));

        private static bool ValidateBookingAvailability(
            DateRange newBookingRange,
            Booking storageBooking)
        {
            return 
                newBookingRange.IncludesStartDate(storageBooking.Start)
                || newBookingRange.IncludesEndDate(storageBooking.Start.AddDays(storageBooking.Nights))
                || newBookingRange.IsIncludedInRange(
                    startDate: storageBooking.Start,
                    endDate: storageBooking.Start.AddDays(storageBooking.Nights));
        }

        private static void ValidateNightsArePositive(int nights) =>
            Validate((Rule: IsNightsInvalid(nights), Parameter: nameof(nights)));

        private static void ValidateUnitsAvailability(int unitsBooked, Rental storageRental)
        {
            if (unitsBooked >= storageRental.Units)
            {
                throw new RentalNotAvailableException(storageRental.Id);
            }
        }

        private static void ValidateRentals(IQueryable<Rental> rentals)
        {
            if (rentals == null || !rentals.Any())
            {
                throw new NotFoundRentalException();
            }
        }

        private static void ValidateBookingIsNotNull(BookingBindingModel booking)
        {
            if (booking is null)
            {
                throw new NullBookingException();
            }
        }

        private static void ValidateStorageBooking(Booking maybeBooking, int bookingId)
        {
            if (maybeBooking is null)
            {
                throw new NotFoundBookingException(bookingId);
            }
        }

        private static void ValidateRentalIsNull(Rental rental)
        {
            if (rental is null)
            {
                throw new NullRentalException();
            }
        }

        private static dynamic IsInvalid(int intValue) => new
        {
            Condition = intValue <= 0,
            Message = $"Value is required"
        };

        private static dynamic IsNightsInvalid(int nights) => new
        {
            Condition = nights <= 0,
            Message = $"Nights must be positive"
        };


        private static dynamic IsInvalid(DateTime date) => new
        {
            Condition = date == default,
            Message = $"Date is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidBookingException = new InvalidBookingException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidBookingException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidBookingException.ThrowIfContainsErrors();
        }
    }
}
