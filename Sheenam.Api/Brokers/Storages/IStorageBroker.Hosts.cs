//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Linq;
using System;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.Hosts;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Host> InsertHostAsync(Host host);
        IQueryable<Host> SelectAllHosts();
        ValueTask<Host> SelectHostByIdAsync(Guid hostId);
        ValueTask<Host> UpdateHostAsync(Host host);
    }
}
