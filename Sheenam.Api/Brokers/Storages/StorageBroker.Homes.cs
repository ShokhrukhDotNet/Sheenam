//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Homes;
using static System.Reflection.Metadata.BlobBuilder;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home) =>
            await InsertAsync(home);

        public IQueryable<Home> SelectAllHomes()
        {
            var homes = SelectAll<Home>().Include(a => a.Host);

            return homes;
        }

        public async ValueTask<Home> SelectHomeByIdAsync(Guid homeId)
        {
            var hostWithHomes = Homes
            .Include(a => a.Host)
                .FirstOrDefault(a => a.HomeId == homeId);

            return await ValueTask.FromResult(hostWithHomes);
        }

        public async ValueTask<Home> UpdateHomeAsync(Home home) =>
            await UpdateAsync(home);

        public async ValueTask<Home> DeleteHomeAsync(Home home) =>
            await DeleteAsync(home);
    }
}
