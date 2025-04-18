//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.Hosts;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Sheenam.Api.Services.Processings.Hosts
{
    public interface IHostProcessingService
    {
        ValueTask<Host> RegisterAndSaveHostAsync(Host host);
        IQueryable<Host> RetrieveAllHosts();
        ValueTask<Host> RetrieveHostByIdAsync(Guid hostId);
        ValueTask<Host> ModifyHostAsync(Host host);
        ValueTask<Host> RemoveHostByIdAsync(Guid hostId);
    }
}