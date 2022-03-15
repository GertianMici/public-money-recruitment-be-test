using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Services.Foundations.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Processings.Rentals
{
    public partial class RentalProcessingService : IRentalProcessingService
    {
        private readonly IRentalService rentalService;
        private readonly ILoggingBroker loggingBroker;

        public RentalProcessingService(
            IRentalService rentalService,
            ILoggingBroker loggingBroker)
        {
            this.loggingBroker = loggingBroker;
            this.rentalService = rentalService;
        }


        public ValueTask<ResourceIdViewModel> AddRentalAsync(RentalBindingModel rentalModel) =>
            TryCatch(async () =>
            {
                ValidateRentalOnAdd(rentalModel);

                var rental = new Rental
                {
                    Units = rentalModel.Units,
                    PreparationTimeInDays = rentalModel.PreparationTimeInDays
                };

                Rental storageRental =
                    await this.rentalService.AddRentalAsync(rental);

                return new ResourceIdViewModel
                {
                    Id = storageRental.Id
                };
            });

        public IQueryable<Rental> RetrieveAllRentals() =>
        TryCatch(() => this.rentalService.RetrieveAllRentals());

        public ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId) =>
            TryCatch(async () =>
            {
                ValidateRentalId(rentalId);

                Rental maybeRental =
                    await this.rentalService.RetrieveRentalByIdAsync(rentalId);

                ValidateStorageRental(maybeRental, rentalId);

                return maybeRental;
            });

        public ValueTask<Rental> ModifyRentalAsync(int rentalId, RentalBindingModel rentalModel) =>
            TryCatch(async () =>
            {
                ValidateRentalId(rentalId);
                ValidateRentalOnModify(rentalModel);

                Rental storageRental =
                    await this.rentalService.RetrieveRentalByIdAsync(rentalId);

                ValidateStorageRental(storageRental, rentalId);

                storageRental.Units = rentalModel.Units;
                storageRental.PreparationTimeInDays = rentalModel.PreparationTimeInDays;

                Rental modifiedRental =
                    await this.rentalService.ModifyRentalAsync(storageRental);

                return modifiedRental;
            });
    }
}
