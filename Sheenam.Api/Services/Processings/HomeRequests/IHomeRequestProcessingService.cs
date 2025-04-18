//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Services.Processings.HomeRequests
{
    public interface IHomeRequestProcessingService
    {
        ValueTask<HomeRequest> RegisterAndSaveHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> RetrieveAllHomeRequests();
        ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId);
        ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest);
        ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId);
    }
}
