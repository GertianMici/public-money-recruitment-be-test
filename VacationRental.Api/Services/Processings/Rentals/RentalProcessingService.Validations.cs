using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Models.Rentals.Exceptions;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Rentals
{
    public partial class RentalProcessingService
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

        public void ValidateRentalId(int rentalId) =>
           Validate((Rule: IsInvalid(rentalId), Parameter: nameof(Rental.Id)));

        private static void ValidateRentalIsNotNull(Rental rental)
        {
            if (rental is null)
            {
                throw new NullRentalException();
            }
        }

        private static void ValidateRentalResourceIdModelIsNotNull(ResourceIdViewModel rentalResourceIdNodel)
        {
            if (rentalResourceIdNodel is null)
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
    }
}
