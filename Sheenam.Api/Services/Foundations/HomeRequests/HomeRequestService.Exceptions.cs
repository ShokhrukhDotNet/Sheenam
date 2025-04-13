﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Threading.Tasks;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        private delegate ValueTask<HomeRequest> ReturningHomeRequestFunction();

        private async ValueTask<HomeRequest> TryCatch(ReturningHomeRequestFunction returningHomeRequestFunction)
        {
            try
            {
                return await returningHomeRequestFunction();
            }
            catch (NullHomeRequestException nullHomeRequestException)
            {
                throw CreateAndLogValidationException(nullHomeRequestException);
            }
        }

        private HomeRequestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeRequestValidationException =
                    new HomeRequestValidationException(exception);

            this.loggingBroker.LogError(homeRequestValidationException);

            return homeRequestValidationException;
        }
    }
}
