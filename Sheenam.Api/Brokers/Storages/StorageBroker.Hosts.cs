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

        public IQueryable<Host> SelectAllHosts()
        {
            var hosts = SelectAll<Host>().Include(a => a.Homes);

            return hosts;
        }

        public async ValueTask<Host> SelectHostByIdAsync(Guid hostId)
        {
            var hostWithHomes = Hosts
                .Include(c => c.Homes)
                .FirstOrDefault(c => c.Id == hostId);

            return await ValueTask.FromResult(hostWithHomes);
        }

        public async ValueTask<Host> UpdateHostAsync(Host host) =>
            await UpdateAsync(host);

        public async ValueTask<Host> DeleteHostAsync(Host host) =>
            await DeleteAsync(host);
    }
}
