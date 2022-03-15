using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Brokers.Storages;
using VacationRental.Api.Models.Rentals;

namespace VacationRental.Api.Services.Foundations.Rentals
{
    public partial class RentalService : IRentalService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public RentalService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Rental> AddRentalAsync(Rental rental) =>
        TryCatch(async () =>
        {
            ValidateRentalOnAdd(rental);

            return await this.storageBroker.InsertRentalAsync(rental);
        });

        public ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId) =>
        TryCatch(async () =>
        {
            ValidateRentalId(rentalId);

            Rental maybeRental = await this.storageBroker
                .SelectRentalByIdAsync(rentalId);

            ValidateStorageRental(maybeRental, rentalId);

            return maybeRental;
        });

        public IQueryable<Rental> RetrieveAllRentals() =>
        TryCatch(() => this.storageBroker.SelectAllRentals());

        public ValueTask<Rental> ModifyRentalAsync(Rental rental) =>
        TryCatch(async () =>
        {
            ValidateRentalOnModify(rental);

            Rental maybeRental =
                await this.storageBroker.SelectRentalByIdAsync(rental.Id);

            ValidateStorageRental(maybeRental, rental.Id);

            maybeRental.PreparationTimeInDays = rental.PreparationTimeInDays;
            maybeRental.Units = rental.Units;

            return await this.storageBroker.UpdateRentalAsync(rental);
        });

        public ValueTask<Rental> RemoveRentalByIdAsync(int rentalId) =>
        TryCatch(async () =>
        {
            ValidateRentalId(rentalId);

            Rental maybeRental = await this.storageBroker
                .SelectRentalByIdAsync(rentalId);

            ValidateStorageRental(maybeRental, rentalId);

            return await this.storageBroker
                .DeleteRentalAsync(maybeRental);
        });
    }
}
