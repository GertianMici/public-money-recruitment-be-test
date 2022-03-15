using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using VacationRental.Api.Models.Bookings;
using VacationRental.Api.Models.Rentals;
using VacationRental.Api.Tests.Brokers;
using VacationRental.Api.ViewModels;
using Xunit;

namespace VacationRental.Api.Tests.Apis.Bookings
{
    [Collection(nameof(ApiTestCollection))]
    public partial class BookingsApiTests
    {
        private readonly ApiBroker apiBroker;

        public BookingsApiTests(ApiBroker apiBroker) =>
            this.apiBroker = apiBroker;

        private async ValueTask<Booking> PostRandomBooking()
        {

            BookingBindingModel randomBooking = await CreateRandomBooking();
            var expectedBooking = new Booking
            {
                Nights = randomBooking.Nights,
                Start = randomBooking.Start,
                RentalId = randomBooking.RentalId
            };

            var postHttpResponseMessage =
                await this.apiBroker.PostBookingAsync(randomBooking);

            ResourceIdViewModel resourceIdViewModel = await
                DeserializeResponseContent<ResourceIdViewModel>(postHttpResponseMessage);

            expectedBooking.Id = resourceIdViewModel.Id;

            return expectedBooking;
        }

        private async ValueTask<BookingBindingModel> CreateRandomBooking()
        {
            int rentalId = await PostRandomRental();

            return new BookingBindingModel { 
                Nights = GetRandomNumber(),
                RentalId = rentalId,
                Start = GetRandomDateTime()
            };
        }

        private async ValueTask<int> PostRandomRental()
        {
            RentalBindingModel randomRental = CreateRandomRentalModel();

            var postHttpResponseMessage =
                await this.apiBroker.PostRentalAsync(randomRental);

            ResourceIdViewModel resourceIdViewModel = await
                DeserializeResponseContent<ResourceIdViewModel>(postHttpResponseMessage);

            return resourceIdViewModel.Id;
        }
        
        private static DateTime GetRandomDateTime() =>
           new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private RentalBindingModel CreateRandomRentalModel()
        {
            return new RentalBindingModel
            {
                PreparationTimeInDays = GetRandomNumber(),
                Units = GetRandomNumber()
            };
        }

        private int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static async ValueTask<T> DeserializeResponseContent<T>(
            HttpResponseMessage responseMessage)
        {
            return await ApiBroker.DeserializeResponseContent<T>(responseMessage);
        }
    }
}
