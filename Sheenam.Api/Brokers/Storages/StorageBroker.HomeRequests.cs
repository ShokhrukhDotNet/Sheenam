﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.HomeRequests;
using static System.Reflection.Metadata.BlobBuilder;

namespace Sheenam.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<HomeRequest> HomeRequests { get; set; }

        public async ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest) =>
            await InsertAsync(homeRequest);

        public IQueryable<HomeRequest> SelectAllHomeRequests()
        {
            var homeRequests = SelectAll<HomeRequest>()
                .Include(a => a.Guest)
                .Include(a => a.Home)
                    .ThenInclude(h => h.Host);

            return homeRequests;
        }

        public async ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid homeRequestId)
        {
            var homeRequestWithGuestAndHome = HomeRequests
            .Include(a => a.Guest)
            .Include(a => a.Home)
                .ThenInclude(h => h.Host)
                    .FirstOrDefault(a => a.Id == homeRequestId);

            return await ValueTask.FromResult(homeRequestWithGuestAndHome);
        }

        public async ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest) =>
            await UpdateAsync(homeRequest);

        public async ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest) =>
            await DeleteAsync(homeRequest);
    }
}
