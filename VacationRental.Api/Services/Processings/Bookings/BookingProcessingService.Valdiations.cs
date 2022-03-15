﻿using System;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Bookings.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Bookings
{
    public partial class BookingProcessingService
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

        public void ValidateBookingId(int bookingId) =>
           Validate((Rule: IsInvalid(bookingId), Parameter: nameof(Booking.Id)));

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

        private static dynamic IsInvalid(int intValue) => new
        {
            Condition = intValue <= 0,
            Message = $"Value is required"
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
