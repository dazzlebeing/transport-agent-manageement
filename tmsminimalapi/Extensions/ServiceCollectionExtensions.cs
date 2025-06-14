using tmsminimalapi.Services.Implementations;
using tmsminimalapi.Services.Interfaces;

namespace tmsminimalapi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPartyService, PartyService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingAssignmentService, BookingAssignmentService>();
            services.AddScoped<ICityService, CityService>();

            return services;
        }
    }
} 