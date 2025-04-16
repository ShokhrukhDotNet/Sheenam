//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Xeptions;

namespace Sheenam.Api.Services.Foundations.HomeRequests
{
    public partial class HomeRequestService
    {
        private delegate ValueTask<HomeRequest> ReturningHomeRequestFunction();
        private delegate IQueryable<HomeRequest> ReturningHomeRequestsFunction();

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
            catch (InvalidHomeRequestException invalidHomeRequestException)
            {
                throw CreateAndLogValidationException(invalidHomeRequestException);
            }
            catch (SqlException sqlException)
            {
                var failedHomeRequestStorageException = new FailedHomeRequestStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedHomeRequestStorageException);
            }
            catch (NotFoundHomeRequestException notFoundHomeRequestException)
            {
                throw CreateAndLogValidationException(notFoundHomeRequestException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedHomeRequestException = new LockedHomeRequestException(dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedHomeRequestException);
            }
            catch (DbUpdateException dbUpdateException)
            {
                var failedHomeRequestStorageException = new FailedHomeRequestStorageException(dbUpdateException);

                throw CreateAndLogDependencyException(failedHomeRequestStorageException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistHomeRequestException =
                    new AlreadyExistHomeRequestException(duplicateKeyException);

                throw CreateAndLogDependencyValidationException(alreadyExistHomeRequestException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);

                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }

        private IQueryable<HomeRequest> TryCatch(ReturningHomeRequestsFunction returningHomeRequestsFunction)
        {
            try
            {
                return returningHomeRequestsFunction();
            }
            catch (SqlException sqlException)
            {
                var failedHomeRequestStorageException =
                    new FailedHomeRequestStorageException(sqlException);

                throw CreateAndLogCriticalDependencyException(failedHomeRequestStorageException);
            }
            catch (Exception exception)
            {
                var failedHomeRequestServiceException =
                    new FailedHomeRequestServiceException(exception);

                throw CreateAndLogServiceException(failedHomeRequestServiceException);
            }
        }

        private HomeRequestValidationException CreateAndLogValidationException(Xeption exception)
        {
            var homeRequestValidationException =
                    new HomeRequestValidationException(exception);

            this.loggingBroker.LogError(homeRequestValidationException);

            return homeRequestValidationException;
        }

        private HomeRequestDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var homeRequestDependencyException = new HomeRequestDependencyException(exception);
            this.loggingBroker.LogCritical(homeRequestDependencyException);

            return homeRequestDependencyException;
        }

        private HomeRequestDependencyValidationException CreateAndLogDependencyValidationException(
            Xeption exception)
        {
            var homeRequestDependencyValidationException =
                new HomeRequestDependencyValidationException(exception);

            this.loggingBroker.LogError(homeRequestDependencyValidationException);

            return homeRequestDependencyValidationException;
        }

        private HomeRequestDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var homeRequestDependencyException = new HomeRequestDependencyException(exception);
            this.loggingBroker.LogError(homeRequestDependencyException);

            return homeRequestDependencyException;
        }

        private HomeRequestServiceException CreateAndLogServiceException(Xeption exception)
        {
            var homeRequestServiceException = new HomeRequestServiceException(exception);
            this.loggingBroker.LogError(homeRequestServiceException);

            return homeRequestServiceException;
        }
    }
}
