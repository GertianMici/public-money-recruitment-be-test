using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetExContexts;

namespace VacationRental.Api.Brokers.Storages
{
    public partial class StorageBroker : NetExContext, IStorageBroker
    {
        private readonly IConfiguration configuration;

        public StorageBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = this.configuration
                .GetConnectionString(name: "DefaultConnection");

            optionsBuilder.UseInMemoryDatabase(connectionString);
        }

        public override void Dispose() { }
    }
}
