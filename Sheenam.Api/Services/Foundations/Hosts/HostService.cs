﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Threading.Tasks;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.Hosts;

namespace Sheenam.Api.Services.Foundations.Hosts
{
    public class HostService : IHostService
    {
        private readonly IStorageBroker storageBroker;

        public HostService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<Host> AddHostAsync(Host host) =>
            throw new NotImplementedException();
    }
}
