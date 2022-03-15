using VacationRental.Api.Tests.Brokers;
using Xunit;

namespace VacationRental.Api.Tests.Apis.Calendars
{
    [Collection(nameof(ApiTestCollection))]
    public partial class CalendarsApiTests
    {
        private readonly ApiBroker apiBroker;

        public CalendarsApiTests(ApiBroker apiBroker) =>
            this.apiBroker = apiBroker;
    }
}
