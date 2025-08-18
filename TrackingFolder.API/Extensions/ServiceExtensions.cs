using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TrackingFolder.API.AppContext;
using TrackingFolder.API.Exceptions;
using TrackingFolder.API.Interfaces;
using TrackingFolder.API.Services;

namespace TrackingFolder.API.Extensions
{   
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceExtensions
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            if (builder.Configuration == null) throw new ArgumentNullException(nameof(builder.Configuration));

            // Adding the database context
            builder.Services.AddDbContext<ApplicationContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
            });

            // Register services in the dependency injection container

            // Adding validators from the current assembly
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Adding GoldExpertMeasureService as a scoped service
            builder.Services.AddScoped<IGoldExpertService, GoldExpertMeasureService>();

            // Adding Log ExceptionHandler 
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();
        }
    }
}
