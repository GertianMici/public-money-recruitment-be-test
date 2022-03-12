using System;
using System.Threading.Tasks;
using VacationRental.Api.Tests.Brokers;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis
{
    [Collection(nameof(ApiTestCollection))]
    public class PostBookingTests
    {
        private readonly ApiBroker apiBroker;

        public PostBookingTests(ApiBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 4
            };

            var postRentalResponse =
                await apiBroker.PostRentalAsync(postRentalRequest);

            Assert.True(postRentalResponse.IsSuccessStatusCode);

            ResourceIdViewModel postRentalResult =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postRentalResponse);

            var postBookingRequest = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 3,
                Start = new DateTime(2001, 01, 01)
            };

            var postBookingResponse =
                await apiBroker.PostBookingAsync(postBookingRequest);

            Assert.True(postBookingResponse.IsSuccessStatusCode);

            ResourceIdViewModel postBookingResult =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postBookingResponse);

            var getBookingResponse =
                await apiBroker.GetBookingByIdAsync(postBookingResult.Id);

            Assert.True(getBookingResponse.IsSuccessStatusCode);

            BookingViewModel getBookingResult =
                await ApiBroker.DeserializeResponseContent<BookingViewModel>(getBookingResponse);

            Assert.Equal(postBookingRequest.RentalId, getBookingResult.RentalId);
            Assert.Equal(postBookingRequest.Nights, getBookingResult.Nights);
            Assert.Equal(postBookingRequest.Start, getBookingResult.Start);
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 1
            };

            var postRentalResponse =
                await apiBroker.PostRentalAsync(postRentalRequest);

            Assert.True(postRentalResponse.IsSuccessStatusCode);

            ResourceIdViewModel postRentalResult =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postRentalResponse);

            var postBooking1Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 3,
                Start = new DateTime(2002, 01, 01)
            };

            var postBooking1Response =
                await apiBroker.PostBookingAsync(postBooking1Request);

            Assert.True(postBooking1Response.IsSuccessStatusCode);


            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 1,
                Start = new DateTime(2002, 01, 02)
            };

            var postBooking2Response =
                    await apiBroker.PostBookingAsync(postBooking2Request);

            Assert.False(postBooking2Response.IsSuccessStatusCode);
        }
    }
}
