﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Services.Foundations.Hosts;

namespace Sheenam.Api.Services.Processings.Hosts
{
    public class HostProcessingService : IHostProcessingService
    {
        private readonly IHostService hostService;

        public HostProcessingService(IHostService hostService) =>
            this.hostService = hostService;

        public async ValueTask<Host> RegisterAndSaveHostAsync(Host host)
        {
            host.Id = Guid.NewGuid();
            host.Homes = new List<Home>();

            return await this.hostService.AddHostAsync(host);
        }

        public IQueryable<Host> RetrieveAllHosts() =>
            this.hostService.RetrieveAllHosts();

        public async ValueTask<Host> RetrieveHostByIdAsync(Guid hostId) =>
            await this.hostService.RetrieveHostByIdAsync(hostId);

        public async ValueTask<Host> ModifyHostAsync(Host host) =>
            await this.hostService.ModifyHostAsync(host);

        public async ValueTask<Host> RemoveHostByIdAsync(Guid hostId) =>
            await this.hostService.RemoveHostByIdAsync(hostId);
    }
}
