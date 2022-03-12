using System;
using System.Threading.Tasks;
using VacationRental.Api.Models;
using VacationRental.Api.Tests.Brokers;
using Xunit;

namespace VacationRental.Api.Tests.Apis
{
    [Collection(nameof(ApiTestCollection))]
    public class GetCalendarTests
    {
        private readonly ApiBroker apiBroker;

        public GetCalendarTests(ApiBroker apiBroker)
        {
            this.apiBroker = apiBroker;
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenGetCalendar_ThenAGetReturnsTheCalculatedCalendar()
        {
            var postRentalRequest = new RentalBindingModel
            {
                Units = 2
            };

            var postRentalResponse =
                await apiBroker.PostRentalAsync(postRentalRequest);

            Assert.True(postRentalResponse.IsSuccessStatusCode);

            ResourceIdViewModel postRentalResult =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postRentalResponse);


            var postBooking1Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 02)
            };


            var postBooking1Response =
                await apiBroker.PostBookingAsync(postBooking1Request);

            Assert.True(postBooking1Response.IsSuccessStatusCode);

            ResourceIdViewModel postBooking1Result =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postBooking1Response);


            var postBooking2Request = new BookingBindingModel
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 03)
            };

            var postBooking2Response =
                await apiBroker.PostBookingAsync(postBooking2Request);

            Assert.True(postBooking2Response.IsSuccessStatusCode);

            ResourceIdViewModel postBooking2Result =
                await ApiBroker.DeserializeResponseContent<ResourceIdViewModel>(postBooking2Response);

            var getCalendarResponse =
                await apiBroker.GetCalendarByRentalIdAsync(
                    rentalId: postRentalResult.Id,
                    date: "2000-01-01",
                    nights: 5);

            Assert.True(getCalendarResponse.IsSuccessStatusCode);

            CalendarViewModel getCalendarResult =
                await ApiBroker.DeserializeResponseContent<CalendarViewModel>(getCalendarResponse);

            Assert.Equal(postRentalResult.Id, getCalendarResult.RentalId);
            Assert.Equal(5, getCalendarResult.Dates.Count);

            Assert.Equal(new DateTime(2000, 01, 01), getCalendarResult.Dates[0].Date);
            Assert.Empty(getCalendarResult.Dates[0].Bookings);

            Assert.Equal(new DateTime(2000, 01, 02), getCalendarResult.Dates[1].Date);
            Assert.Single(getCalendarResult.Dates[1].Bookings);
            Assert.Contains(getCalendarResult.Dates[1].Bookings, x => x.Id == postBooking1Result.Id);

            Assert.Equal(new DateTime(2000, 01, 03), getCalendarResult.Dates[2].Date);
            Assert.Equal(2, getCalendarResult.Dates[2].Bookings.Count);
            Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking1Result.Id);
            Assert.Contains(getCalendarResult.Dates[2].Bookings, x => x.Id == postBooking2Result.Id);

            Assert.Equal(new DateTime(2000, 01, 04), getCalendarResult.Dates[3].Date);
            Assert.Single(getCalendarResult.Dates[3].Bookings);
            Assert.Contains(getCalendarResult.Dates[3].Bookings, x => x.Id == postBooking2Result.Id);

            Assert.Equal(new DateTime(2000, 01, 05), getCalendarResult.Dates[4].Date);
            Assert.Empty(getCalendarResult.Dates[4].Bookings);

        }
    }
}
