//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.Hosts;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Host> Hosts { get; set; }

        public async ValueTask<Host> InsertHostAsync(Host host) =>
            await InsertAsync(host);

        public IQueryable<Host> SelectAllHosts() => SelectAll<Host>();

        public async ValueTask<Host> SelectHostByIdAsync(Guid hostId) =>
             await SelectAsync<Host>(hostId);

        public async ValueTask<Host> UpdateHostAsync(Host host) =>
            await UpdateAsync(host);

        public async ValueTask<Host> DeleteHostAsync(Host host) =>
            await DeleteAsync(host);
    }
}
