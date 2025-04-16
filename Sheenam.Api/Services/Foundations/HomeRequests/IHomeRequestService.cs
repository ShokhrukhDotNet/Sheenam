//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Linq;
using System.Threading.Tasks;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public interface IHomeRequestService
    {
        ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest);
        IQueryable<HomeRequest> RetrieveAllHomeRequests();
    }
}
