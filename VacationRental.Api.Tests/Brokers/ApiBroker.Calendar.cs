using System.Net.Http;
using System.Threading.Tasks;

namespace VacationRental.Api.Tests.Brokers
{
    public partial class ApiBroker
    {
        private const string CalendarRelativeUrl = "/api/v1/calendar";

        public async ValueTask<HttpResponseMessage> GetCalendarByRentalIdAsync(int rentalId, string date, int nights)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync($"{CalendarRelativeUrl}?rentalId={rentalId}&start={date}&nights={nights}");

            return responseMessage;
        }

    }
}
