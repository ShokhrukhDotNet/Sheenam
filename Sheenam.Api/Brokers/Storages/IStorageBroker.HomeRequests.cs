﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> SelectAllHomeRequests();
        ValueTask<HomeRequest> SelectHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> UpdateHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> DeleteHomeRequestAsync(HomeRequest homeRequest);
    }
}
