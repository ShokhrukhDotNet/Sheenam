//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Home> Homes { get; set; }

        public async ValueTask<Home> InsertHomeAsync(Home home) =>
            await InsertAsync(home);

        public IQueryable<Home> SelectAllHomes() => SelectAll<Home>();

        public async ValueTask<Home> SelectHomeByIdAsync(Guid homeId) =>
            await SelectAsync<Home>(homeId);

        public async ValueTask<Home> UpdateHomeAsync(Home home) =>
            await UpdateAsync(home);
    }
}
