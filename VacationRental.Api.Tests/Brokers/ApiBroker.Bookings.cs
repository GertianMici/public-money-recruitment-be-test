using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Tests.Brokers
{
    public partial class ApiBroker
    {
        private const string BookingsRelativeUrl = "/api/v1/bookings";

        public async ValueTask<HttpResponseMessage> PostBookingAsync(BookingBindingModel model)
        {
            StringContent contentString = StringifyJsonifyContent(model, "text/json");

            HttpResponseMessage responseMessage =
                await this.httpClient.PostAsync(BookingsRelativeUrl, contentString);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetBookingByIdAsync(int bookingId)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync($"{BookingsRelativeUrl}/{bookingId}");

            return responseMessage;
        }

    }
}
