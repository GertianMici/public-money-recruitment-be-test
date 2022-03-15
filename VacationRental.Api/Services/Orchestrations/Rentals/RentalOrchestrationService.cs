using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Helpers.DateRange;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Services.Processings.Bookings;
using VacationRental.Api.Services.Processings.Rentals;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Services.Orchestrations.Rentals
{
    public partial class RentalOrchestrationService : IRentalOrchestrationService
    {

        private readonly IBookingProcessingService bookingProcessingService;
        private readonly IRentalProcessingService rentalProcessingService;
        private readonly ILoggingBroker loggingBroker;

        public RentalOrchestrationService(
            IBookingProcessingService bookingProcessingService,
            IRentalProcessingService rentalProcessingService,
            ILoggingBroker loggingBroker)
        {
            this.bookingProcessingService = bookingProcessingService;
            this.rentalProcessingService = rentalProcessingService;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<ResourceIdViewModel> AddRentalAsync(RentalBindingModel rentalModel) =>
            TryCatch(async () =>
            {

                ValidateRentalOnAdd(rentalModel);

                return await this.rentalProcessingService.AddRentalAsync(rentalModel);
            });

        public ValueTask<Rental> RetrieveRentalByIdAsync(int rentalId) =>
            TryCatch(async () =>
            {
                ValidateRentalId(rentalId);

                Rental maybeRental =
                    await this.rentalProcessingService.RetrieveRentalByIdAsync(rentalId);

                ValidateStorageRental(maybeRental, rentalId);

                return maybeRental;
            });

        public ValueTask<Rental> ModifyRentalAsync(int rentalId, RentalBindingModel rentalModel) =>
            TryCatch(async () =>
            {
                ValidateRentalOnModify(rentalModel);

                Rental maybeRental =
                    await this.rentalProcessingService.RetrieveRentalByIdAsync(rentalId);

                ValidateStorageRental(maybeRental, rentalId);

                IQueryable<Booking> allBookingsForGivenRental =
                    this.bookingProcessingService.RetrieveAllBookings()
                        .Where(booking => booking.RentalId == maybeRental.Id);

                //only affects the bookings if the preparation time is increased
                //or units are decreased
                if (rentalModel.PreparationTimeInDays > maybeRental.PreparationTimeInDays ||
                    rentalModel.Units < maybeRental.Units)
                {
                    foreach (Booking booking in allBookingsForGivenRental)
                    {
                        ValidateCanModifyRental(
                            booking,
                            allBookingsForGivenRental,
                            rentalModel.PreparationTimeInDays,
                            rentalModel.Units);
                    }
                }


                Rental modifiedRental =
                   await this.rentalProcessingService.ModifyRentalAsync(rentalId, rentalModel);

                return modifiedRental;
            });

    }
}
