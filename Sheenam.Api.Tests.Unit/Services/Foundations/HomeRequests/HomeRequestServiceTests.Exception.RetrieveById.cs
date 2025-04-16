//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedHomeRequestStorageException =
                new FailedHomeRequestStorageException(sqlException);

            HomeRequestDependencyException expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedHomeRequestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<HomeRequest> retrieveHomeRequestById =
                this.homeRequestService.RetrieveHomeRequestByIdAsync(someId);

            HomeRequestDependencyException actualHomeRequestDependencyException =
                await Assert.ThrowsAsync<HomeRequestDependencyException>(
                    retrieveHomeRequestById.AsTask);

            // then
            actualHomeRequestDependencyException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(someId), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
