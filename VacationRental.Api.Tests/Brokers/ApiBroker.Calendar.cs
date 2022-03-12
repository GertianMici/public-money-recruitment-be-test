using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VacationRental.Api.Models.Calendars;

namespace VacationRental.Api.Tests.Brokers
{
    public partial class ApiBroker
    {
        private const string CalendarRelativeUrl = "/api/v1/calendar";

        public async ValueTask<HttpResponseMessage> PostCalendarAsync(Calendar model)
        {
            StringContent contentString = StringifyJsonifyContent(model, "text/json");

            HttpResponseMessage responseMessage =
                await this.httpClient.PostAsync(CalendarRelativeUrl, contentString);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetAllCalendarsAsync()
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync(CalendarRelativeUrl);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetCalendarByIdAsync(int calendarId)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync($"{CalendarRelativeUrl}/{calendarId}");

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> GetCalendarByRentalIdAsync(int rentalId, string date, int nights)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync($"{CalendarRelativeUrl}?rentalId={rentalId}&start={date}&nights={nights}");

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> PutCalendarAsync(Calendar model)
        {
            StringContent contentString = StringifyJsonifyContent(model, "text/json");

            HttpResponseMessage responseMessage =
               await this.httpClient.PutAsync(CalendarRelativeUrl, contentString);

            return responseMessage;
        }

        public async ValueTask<HttpResponseMessage> DeleteCalendarByIdAsync(int calendarId)
        {
            HttpResponseMessage responseMessage =
               await this.httpClient.DeleteAsync($"{CalendarRelativeUrl}/{calendarId}");

            return responseMessage;
        }
    }
}
