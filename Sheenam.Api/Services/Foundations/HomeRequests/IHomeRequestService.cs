﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public interface IHomeRequestService
    {
        ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> RetrieveAllHomeRequests();
        ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest);
    }
}
