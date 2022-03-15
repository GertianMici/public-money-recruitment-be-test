using System.Linq;
using VacationRental.Api.Helpers.DateRange;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Rentals
{
    public partial class RentalOrchestrationService
    {

        private static void ValidateRentalOnAdd(RentalBindingModel rentalModel)
        {
            Validate(
                (Rule: IsInvalid(rentalModel.Units), Parameter: nameof(rentalModel.Units)),
                (Rule: IsNegative(rentalModel.PreparationTimeInDays), Parameter: nameof(rentalModel.PreparationTimeInDays))
                );
        }

        private static void ValidateRentalOnModify(RentalBindingModel rentalModel)
        {
            Validate(
                (Rule: IsInvalid(rentalModel.Units), Parameter: nameof(rentalModel.Units)),
                (Rule: IsNegative(rentalModel.PreparationTimeInDays), Parameter: nameof(rentalModel.PreparationTimeInDays))
                );
        }

        private static dynamic IsInvalid(int value) => new
        {
            Condition = value <= 0,
            Message = $"Value is required"
        };

        private static dynamic IsNegative(int preparationDays) => new
        {
            Condition = preparationDays < 0,
            Message = $"Preparation days can not be negative"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidRentalException = new InvalidRentalException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidRentalException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidRentalException.ThrowIfContainsErrors();
        }

        private static void ValidateStorageRental(Rental maybeRental, int rentalId)
        {
            if (maybeRental is null)
            {
                throw new NotFoundRentalException(rentalId);
            }
        }

        private static void ValidateRentalIsNotNull(RentalBindingModel rental)
        {
            if (rental is null)
            {
                throw new NullRentalException();
            }
        }

        private static void ValidateRentalId(int rentalId) =>
           Validate((Rule: IsInvalid(rentalId), Parameter: nameof(Rental.Id)));

        private void ValidateCanModifyRental(
            Booking storageBooking,
            IQueryable<Booking> allBookings,
            int preparationTimeInDays,
            int units)
        {
            var modifiedBookingRange = new DateRange(
                    storageBooking.Start,
                    storageBooking.Start.AddDays(storageBooking.Nights + preparationTimeInDays));

            int unitsBooked = 0;

            foreach (var booking in allBookings)
            {
                if (HasExistingBookingInDateRange(modifiedBookingRange, booking, preparationTimeInDays))
                {
                    if (storageBooking.Unit != booking.Unit)
                    {
                        unitsBooked++;
                    }

                    if (storageBooking.Unit == booking.Unit &&
                        storageBooking.Id != booking.Id)
                    {

                        throw new InvalidRentalException(
                            message: "Can not modify rental because of overlapping booking dates.");
                    }
                }
            }

            ValidateUnitsAvailability(unitsBooked, units);
        }

        private static bool HasExistingBookingInDateRange(
            DateRange newBookingRange,
            Booking storageBooking,
            int preparationTimeInDays)
        {
            return
                newBookingRange.IncludesStartDate(
                    startDate: storageBooking.Start)
                || newBookingRange.IncludesEndDate(
                    endDate: storageBooking.Start.AddDays(storageBooking.Nights + preparationTimeInDays))
                || newBookingRange.IsIncludedInRange(
                    startDate: storageBooking.Start,
                    endDate: storageBooking.Start.AddDays(storageBooking.Nights + preparationTimeInDays));
        }

        private static bool ValidateUnitsAvailability(int unitsBooked, int units)
        {
            if (unitsBooked > units)
            {
                throw new InvalidRentalException(
                    message: "Can not decrease number of units because of overlapping booking dates.");
            }

            return true;
        }
    }
}
