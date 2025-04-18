﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.Homes;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Sheenam.Api.Services.Processings.Homes
{
    public interface IHomeProcessingService
    {
        ValueTask<Home> RegisterAndSaveHomeAsync(Home home);
        IQueryable<Home> RetrieveAllHomes();
        ValueTask<Home> RetrieveHomeByIdAsync(Guid homeId);
        ValueTask<Home> ModifyHomeAsync(Home home);
        ValueTask<Home> RemoveHomeByIdAsync(Guid homeId);
    }
}
