using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VacationRental.Api.Brokers.Loggings;
using VacationRental.Api.Brokers.Storages;
using VacationRental.Api.Services.Foundations.Bookings;
using VacationRental.Api.Services.Foundations.Rentals;
using VacationRental.Api.Services.Orchestrations.Bookings;
using VacationRental.Api.Services.Orchestrations.Rentals;
using VacationRental.Api.Services.Processings.Bookings;
using VacationRental.Api.Services.Processings.Rentals;

namespace VacationRental.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddControllers();
            services.AddDbContext<StorageBroker>();

            AddBrokers(services);
            AddFoundationServices(services);
            AddProcessingServices(services);
            AddOrchestrationServices(services);

            services.AddSwaggerGen(options =>
            {
                var openApiInfo = new OpenApiInfo
                {
                    Title = "Vacation rental information",
                    Version = "v1"
                };

                options.SwaggerDoc(name: "v1", info: openApiInfo);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                    url: "/swagger/v1/swagger.json",
                    name: "VacationRental v1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IRentalService, RentalService>();
        }

        private static void AddProcessingServices(IServiceCollection services)
        {
            services.AddTransient<IRentalProcessingService, RentalProcessingService>();
            services.AddTransient<IBookingProcessingService, BookingProcessingService>();
        }
        private static void AddOrchestrationServices(IServiceCollection services)
        {
            services.AddTransient<IBookingRentalOrchestrationService, BookingRentalOrchestrationService>();
            services.AddTransient<IRentalOrchestrationService, RentalOrchestrationService>();
        }
    }
}
