﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using Sheenam.Api.Brokers.DateTimes;
using Sheenam.Api.Brokers.Loggings;
using Sheenam.Api.Brokers.Storages;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService : IHomeRequestService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeRequestService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<HomeRequest> AddHomeRequestAsync(HomeRequest homeRequest) =>
        TryCatch(async () =>
        {
            ValidateHomeRequestOnAdd(homeRequest);

            return await this.storageBroker.InsertHomeRequestAsync(homeRequest);
        });

        public ValueTask<HomeRequest> RetrieveHomeRequestByIdAsync(Guid homeRequestId) =>
        TryCatch(async () =>
        {
            ValidateHomeRequestId(homeRequestId);

            HomeRequest maybeHomeRequest = await this.storageBroker.SelectHomeRequestByIdAsync(homeRequestId);

            ValidateStorageHomeRequest(maybeHomeRequest, homeRequestId);

            return maybeHomeRequest;
        });

        public IQueryable<HomeRequest> RetrieveAllHomeRequests() =>
            TryCatch(() => this.storageBroker.SelectAllHomeRequests());

        public ValueTask<HomeRequest> ModifyHomeRequestAsync(HomeRequest homeRequest) =>
        TryCatch(async () =>
        {
            ValidateHomeRequestOnModify(homeRequest);

            HomeRequest maybeHomeRequest =
                await this.storageBroker.SelectHomeRequestByIdAsync(homeRequest.Id);

            ValidateAgainstStorageHomeRequestOnModify(homeRequest, maybeHomeRequest);

            return await this.storageBroker.UpdateHomeRequestAsync(homeRequest);
        });

        public ValueTask<HomeRequest> RemoveHomeRequestByIdAsync(Guid homeRequestId) =>
            throw new NotImplementedException();
    }
}
