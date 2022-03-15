using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis.Bookings
{
    public partial class BookingsApiTests
    {
        [Fact]
        public async Task ShouldPostBookingAsync()
        {
            //given
            BookingBindingModel randomBooking = await CreateRandomBooking();
            BookingBindingModel inputBooking = randomBooking;

            var expectedBooking = new Booking
            {
                Nights = inputBooking.Nights,
                Start = inputBooking.Start,
                RentalId = inputBooking.RentalId
            };

            //when
            var postHttpResponseMessage =
                await this.apiBroker.PostBookingAsync(randomBooking);

            ResourceIdViewModel resourceIdViewModel = await
                DeserializeResponseContent<ResourceIdViewModel>(postHttpResponseMessage);

            var getHttpResponseMessage =
               await this.apiBroker.GetBookingByIdAsync(resourceIdViewModel.Id);

            Booking actualBooking = await
                DeserializeResponseContent<Booking>(getHttpResponseMessage);

            expectedBooking.Id = resourceIdViewModel.Id;

            //then
            expectedBooking.Unit = actualBooking.Unit;
            actualBooking.Should().BeEquivalentTo(expectedBooking);

        }

        [Fact]
        public async Task ShouldGetBookingByIdAsync()
        {
            //given
            Booking randomBooking = await PostRandomBooking();
            Booking expectedBooking = randomBooking;

            //when
            var getHttpResponseMessage =
                await this.apiBroker.GetBookingByIdAsync(randomBooking.Id);

            Booking actualBooking = await
                DeserializeResponseContent<Booking>(getHttpResponseMessage);

            expectedBooking.Unit = actualBooking.Unit;

            //then
            actualBooking.Should().BeEquivalentTo(expectedBooking);
        }
    }
}
