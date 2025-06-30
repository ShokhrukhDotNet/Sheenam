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
            homeRequest.GuestId = homeRequest.Guest?.Id ?? homeRequest.GuestId;
            homeRequest.HomeId = homeRequest.Home?.HomeId ?? homeRequest.HomeId;

            return await this.homeRequestService.AddHomeRequestAsync(homeRequest);
        }

        public IQueryable<HomeRequest> RetrieveAllHomeRequests() =>
            this.homeRequestService.RetrieveAllHomeRequests();

        public async ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId) =>
            await this.homeRequestService.RetrieveHomeRequestByIdAsync(homeRequestId);

        public async ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest)
        {
            homeRequest.UpdatedDate = DateTimeOffset.UtcNow;

            var updatedHomeRequest = CreateHomeRequest(homeRequest);

            return await this.homeRequestService.ModifyHomeRequestAsync(updatedHomeRequest);
        }

        public async ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId) =>
            await this.homeRequestService.RemoveHomeRequestByIdAsync(homeRequestId);

        private static HomeRequest CreateHomeRequest(HomeRequest inputHomeRequest)
        {
            return new HomeRequest
            {
                Id = inputHomeRequest.Id,
                GuestId = inputHomeRequest.Guest?.Id ?? inputHomeRequest.GuestId,
                HomeId = inputHomeRequest.Home?.HomeId ?? inputHomeRequest.HomeId,
                Message = inputHomeRequest.Message,
                StartDate = inputHomeRequest.StartDate,
                EndDate = inputHomeRequest.EndDate,
                CreatedDate = inputHomeRequest.CreatedDate,
                UpdatedDate = inputHomeRequest.UpdatedDate,
                Guest = inputHomeRequest.Guest,
                Home = inputHomeRequest.Home
            };
        }
    }
}
