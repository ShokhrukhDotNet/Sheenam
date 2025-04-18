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
            return await this.homeService.AddHomeAsync(home);
        }

        public IQueryable<Home> RetrieveAllHomes() =>
            this.homeService.RetrieveAllHomes().Include(home => home.Host);

        public async ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId)
        {
            IQueryable<Home> homesWithHost = this.homeService.RetrieveAllHomes()
                .Where(home => home.HomeId == homeId)
                .Include(home => home.Host);

            return await homesWithHost.FirstOrDefaultAsync();
        }

        public async ValueTask<Home> ModifyHomeAsync(Home home) =>
            await this.homeService.ModifyHomeAsync(home);

        public async ValueTask<Home> RemoveHomeByIdAsync(Guid homeId) =>
            await this.homeService.RemoveHomeByIdAsync(homeId);
    }
}
