using System.Net.Http;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Tests.Brokers;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis.Rentals
{
    [Collection(nameof(ApiTestCollection))]
    public partial class RentalsApiTests
    {
        private readonly ApiBroker apiBroker;

        public RentalsApiTests(ApiBroker apiBroker) =>
            this.apiBroker = apiBroker;


        private RentalBindingModel CreateRandomRentalModel()
        {
            return new RentalBindingModel
            {
                PreparationTimeInDays = GetRandomNumber(),
                Units = GetRandomNumber()
            };
        }

        private RentalBindingModel UpadteRandomRentalModel()
        {
            return new RentalBindingModel
            {
                PreparationTimeInDays = GetRandomNumber(),
                Units = GetRandomNumber()
            };
        }

        private int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private async ValueTask<Rental> PostRandomRental()
        {
            RentalBindingModel randomRental = CreateRandomRentalModel();
            var expectedRental = new Rental
            {
                PreparationTimeInDays = randomRental.PreparationTimeInDays,
                Units = randomRental.Units
            };

            var postHttpResponseMessage =
                await this.apiBroker.PostRentalAsync(randomRental);

            ResourceIdViewModel resourceIdViewModel = await
                DeserializeResponseContent<ResourceIdViewModel>(postHttpResponseMessage);

            expectedRental.Id = resourceIdViewModel.Id;

            return expectedRental;
        }

        private static async ValueTask<T> DeserializeResponseContent<T>(
            HttpResponseMessage responseMessage)
        {
            return await ApiBroker.DeserializeResponseContent<T>(responseMessage);
        }
    }
}
