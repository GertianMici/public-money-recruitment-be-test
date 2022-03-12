using System.Threading.Tasks;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Tests.Brokers;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis
{
    [Collection(nameof(ApiTestCollection))]
    public class PostRentalTests
    {
        private readonly ApiBroker apiBroker;

        public PostRentalTests(ApiBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new RentalBindingModel
            {
                Units = 25
            };

            var postResponse =
                await apiBroker.PostRentalAsync(request);

            Assert.True(postResponse.IsSuccessStatusCode);

            ResourceIdViewModel postResult =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postResponse);

            var getResponse = await apiBroker.GetRentalByIdAsync(postResult.Id);

            Assert.True(getResponse.IsSuccessStatusCode);

            Rental getResult =
                await ApiBroker.DeserializeResponseContent<Rental>(getResponse);

            Assert.Equal(request.Units, getResult.Units);

        }
    }
}
