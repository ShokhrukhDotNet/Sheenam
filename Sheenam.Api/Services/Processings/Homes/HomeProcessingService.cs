//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Services.Foundations.Homes;

namespace Sheenam.Api.Services.Processings.Homes
{
    public class HomeProcessingService : IHomeProcessingService
    {
        private readonly IHomeService homeService;

        public HomeProcessingService(IHomeService homeService) =>
            this.homeService = homeService;

        public async ValueTask<Home> RegisterAndSaveHomeAsync(Home home)
        {
            home.HomeId = Guid.NewGuid();
            home.HostId = home.Host?.Id ?? home.HostId;

            return await this.homeService.AddHomeAsync(home);
        }

        public IQueryable<Home> RetrieveAllHomes() =>
            this.homeService.RetrieveAllHomes();

        public async ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId) =>
            await this.homeService.RetrieveHomeByIdAsync(homeId);

        public async ValueTask<Home> ModifyHomeAsync(Home home)
        {
            var updatedHome = CreateHome(home);

            return await this.homeService.ModifyHomeAsync(updatedHome);
        }

        public async ValueTask<Home> RemoveHomeByIdAsync(Guid homeId) =>
            await this.homeService.RemoveHomeByIdAsync(homeId);

        private static Home CreateHome(Home inputHome)
        {
            return new Home
            {
                HomeId = inputHome.HomeId,
                Address = inputHome.Address,
                AdditionalInfo = inputHome.AdditionalInfo,
                NumberOfBedrooms = inputHome.NumberOfBedrooms,
                NumberOfBathrooms = inputHome.NumberOfBathrooms,
                Area = inputHome.Area,
                Price = inputHome.Price,
                Type = inputHome.Type,
                HostId = inputHome.Host?.Id ?? inputHome.HostId,
                Host = inputHome.Host
            };
        }
    }
}
