using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;

namespace VacationRental.Api.Services.Foundations.Rentals
{
    public partial class RentalService
    {
        private static void ValidateRentalOnAdd(Rental rental)
        {
            ValidateRentalIsNotNull(rental);

            Validate((Rule: IsInvalid(rental.Units), Parameter: nameof(Rental.Units)));
        }

        private static void ValidateRentalOnModify(Rental rental)
        {
            ValidateRentalIsNotNull(rental);

            Validate(
                (Rule: IsInvalid(rental.Id), Parameter: nameof(rental.Id)),
                (Rule: IsInvalid(rental.Units), Parameter: nameof(rental.Units))
                );
        }

        public void ValidateRentalId(int rentalId) =>
           Validate((Rule: IsInvalid(rentalId), Parameter: nameof(Rental.Id)));

        private static void ValidateRentalIsNotNull(Rental rental)
        {
            if (rental is null)
            {
                throw new NullRentalException();
            }
        }

        private static void ValidateStorageRental(Rental maybeRental, int rentalId)
        {
            if (maybeRental is null)
            {
                throw new NotFoundRentalException(rentalId);
            }
        }

        private static dynamic IsInvalid(int intValue) => new
        {
            Condition = intValue <= 0,
            Message = $"Value is required"
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
    }
}
