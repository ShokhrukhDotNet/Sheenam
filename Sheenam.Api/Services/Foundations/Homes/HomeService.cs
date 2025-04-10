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
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Services.Foundations.Homes
{
    public partial class HomeService : IHomeService
    {
        private readonly IStorageBroker storageBroker;
        private readonly IDateTimeBroker dateTimeBroker;
        private readonly ILoggingBroker loggingBroker;

        public HomeService(
            IStorageBroker storageBroker,
            IDateTimeBroker dateTimeBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<Home> AddHomeAsync(Home home) =>
        TryCatch(async () =>
        {
            ValidateHomeOnAdd(home);

            return await this.storageBroker.InsertHomeAsync(home);
        });

        public ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId) =>
        TryCatch(async () =>
        {
            ValidateHomeId(homeId);

            Home maybeHome = await this.storageBroker.SelectHomeByIdAsync(homeId);

            ValidateStorageHome(maybeHome, homeId);

            return maybeHome;
        });

        public IQueryable<Home> RetrieveAllHomes() =>
            TryCatch(() => this.storageBroker.SelectAllHomes());

        public ValueTask<Home> ModifyHomeAsync(Home home) =>
            throw new NotImplementedException();
    }
}