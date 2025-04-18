//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Services.Foundations.HomeRequests;

namespace Sheenam.Api.Services.Processings.HomeRequests
{
    public class HomeRequestProcessingService : IHomeRequestProcessingService
    {
        private readonly IHomeRequestService homeRequestService;

        public HomeRequestProcessingService(IHomeRequestService homeRequestService) =>
            this.homeRequestService = homeRequestService;

        public async ValueTask<HomeRequest> RegisterAndSaveHomeRequestAsync(HomeRequest homeRequest)
        {
            homeRequest.Id = Guid.NewGuid();
            homeRequest.CreatedDate = DateTimeOffset.UtcNow;
            homeRequest.UpdatedDate = homeRequest.CreatedDate;

            return await this.homeRequestService.AddHomeRequestAsync(homeRequest);
        }

        public IQueryable<HomeRequest> RetrieveAllHomeRequests() =>
            this.homeRequestService.RetrieveAllHomeRequests();

        public async ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId) =>
            await this.homeRequestService.RetrieveHomeRequestByIdAsync(homeRequestId);

        public async ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest)
        {
            homeRequest.UpdatedDate = DateTimeOffset.UtcNow;

            return await this.homeRequestService.ModifyHomeRequestAsync(homeRequest);
        }

        public async ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId) =>
            await this.homeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);
    }
}
