using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.ViewModels;

namespace VacationRental.Api.Tests.Brokers
{
    public partial class ApiBroker
    {
        private const string RentalsRelativeUrl = "/api/v1/rentals";

        public async ValueTask<HttpResponseMessage> PostRentalAsync(RentalBindingModel model)
        {
            StringContent contentString = StringifyJsonifyContent(model, "text/json");

            HttpResponseMessage responseMessage =
                await this.httpClient.PostAsync(RentalsRelativeUrl, contentString);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetAllRentalsAsync()
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync(RentalsRelativeUrl);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetRentalByIdAsync(int rentalId)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync($"{RentalsRelativeUrl}/{rentalId}");

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> PutRentalAsync(RentalBindingModel model)
        {
            StringContent contentString = StringifyJsonifyContent(model, "text/json");

            HttpResponseMessage responseMessage =
               await this.httpClient.PutAsync(RentalsRelativeUrl, contentString);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> DeleteRentalByIdAsync(int rentalId)
        {
            HttpResponseMessage responseMessage =
               await this.httpClient.DeleteAsync($"{RentalsRelativeUrl}/{rentalId}");

            return responseMessage;
        }
    }
}
