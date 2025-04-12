//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Threading.Tasks;

namespace Sheenam.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<HomeRequest> InsertHomeRequestAsync(HomeRequest homeRequest);
    }
}
