//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Services.Foundations.Guests;

namespace Sheenam.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.AddDbContext<StorageBroker>();
            AddBrokers(builder);
            AddFoundationServices(builder);

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
            builder.Services.AddTransient<ILoggingBroker, LoggingBroker>();
        }
        
        private static void AddFoundationServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuestService, GuestService>();
        }
    }
}