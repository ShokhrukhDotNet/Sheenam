﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<HomeRequest> HomeRequests { get; set; }

        public async ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest) =>
            await InsertAsync(homeRequest);

        public IQueryable<HomeRequest> SelectAllHomeRequests() => SelectAll<HomeRequest>();

        public async ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid homeRequestId) =>
            await SelectAsync<HomeRequest>(homeRequestId);

        public async ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest) =>
            await UpdateAsync(homeRequest);

        public async ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest) =>
            await DeleteAsync(homeRequest);
    }
}
