﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.Hosts;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public interface IHostService
    {
        ValueTask<Host> AddHostAsync(Host host);
        IQueryable<Host> RetrieveAllHosts();
        ValueTask<Host> RetrieveHostByIdAsync(Guid hostId);
        ValueTask<Host> RemoveHostByIdAsync(Guid locationId);
        ValueTask<Host> ModifyHostAsync(Host host);
    }
}
