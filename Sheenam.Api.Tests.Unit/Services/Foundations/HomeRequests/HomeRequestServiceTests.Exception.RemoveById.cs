//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldThrowDependencyValidationOnRemoveIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            Guid someHomeRequestId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            LockedHomeRequestException lockedHomeRequestException =
                new LockedHomeRequestException(dbUpdateConcurrencyException);

            var expectedHomeRequestDependencyValidationException =
                new HomeRequestDependencyValidationException(lockedHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<HomeRequest> removeHomeRequestById =
                this.homeRequestService.RemoveHomeRequestByIdAsync(someHomeRequestId);

            var actualHomeRequestDependencyValidationException =
                await Assert.ThrowsAsync<HomeRequestDependencyValidationException>(
                    removeHomeRequestById.AsTask);

            // then
            actualHomeRequestDependencyValidationException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(It.IsAny<HomeRequest>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnRemoveWhenSqlExceptionOccursAndLogItAsync()
        {
            // given
            Guid someLocationId = Guid.NewGuid();
            SqlException sqlException = GetSqlError();

            var failedHomeRequestStorageException =
                new FailedHomeRequestStorageException(sqlException);

            var expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedHomeRequestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<HomeRequest> deleteHomeRequestTask =
                this.homeRequestService.RemoveHomeRequestByIdAsync(someLocationId);

            var actualHomeRequestDependencyException =
                await Assert.ThrowsAsync<HomeRequestDependencyException>(
                    deleteHomeRequestTask.AsTask);

            // then
            actualHomeRequestDependencyException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveIfExceptionOccursAndLogItAsync()
        {
            // given
            Guid someHomeRequestId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedHomeRequestServiceException =
                new FailedHomeRequestServiceException(serviceException);

            var expectedHomeRequestServiceException =
                new HomeRequestServiceException(failedHomeRequestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<HomeRequest> removeHomeRequestByIdTask =
                this.homeRequestService.RemoveHomeRequestByIdAsync(someHomeRequestId);

            HomeRequestServiceException actualHomeRequestServiceException =
                await Assert.ThrowsAsync<HomeRequestServiceException>(
                    removeHomeRequestByIdTask.AsTask);

            // then
            actualHomeRequestServiceException.Should()
                .BeEquivalentTo(expectedHomeRequestServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
