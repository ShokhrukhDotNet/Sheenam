//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Services.Foundations.Guests;
using Sheenam.Api.Services.Foundations.HomeRequests;
using Sheenam.Api.Services.Foundations.Homes;
using Sheenam.Api.Services.Foundations.Hosts;
using Sheenam.Api.Services.Processings.Guests;
using Sheenam.Api.Services.Processings.HomeRequests;
using Sheenam.Api.Services.Processings.Homes;
using Sheenam.Api.Services.Processings.Hosts;

namespace Sheenam.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddDbContext<StorageBroker>();
            AddBrokers(builder);
            AddFoundationServices(builder);
            AddProcessingServices(builder);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void AddBrokers(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IStorageBroker, StorageBroker>();
            builder.Services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
        }

        private static void AddFoundationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestService, GuestService>();
            builder.Services.AddTransient<IHostService, HostService>();
            builder.Services.AddTransient<IHomeService, HomeService>();
            builder.Services.AddTransient<IHomeRequestService, HomeRequestService>();
        }

        private static void AddProcessingServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestProcessingService, GuestProcessingService>();
            builder.Services.AddTransient<IHostProcessingService, HostProcessingService>();
            builder.Services.AddTransient<IHomeProcessingService, HomeProcessingService>();
            builder.Services.AddTransient<IHomeRequestProcessingService, HomeRequestProcessingService>();
        }
    }
}