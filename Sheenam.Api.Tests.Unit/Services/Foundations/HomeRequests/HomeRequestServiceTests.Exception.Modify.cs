﻿//==================================================
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
        public async Task ShouldThrowCriticalDependencyExceptionOnModifyIfSqlErrorOccursAndLogItAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest someHomeRequest = randomHomeRequest;
            Guid homeRequestId = someHomeRequest.Id;
            SqlException sqlException = GetSqlError();

            var failedHomeRequestStorageException =
                new FailedHomeRequestStorageException(sqlException);

            var expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedHomeRequestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId)).Throws(sqlException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(someHomeRequest);

            HomeRequestDependencyException actualHomeRequestDependencyException =
                await Assert.ThrowsAsync<HomeRequestDependencyException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestDependencyException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(someHomeRequest), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnModifyIfDatabaseUpdateExceptionOccursAndLogItAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest someHomeRequest = randomHomeRequest;
            Guid homeRequestId = someHomeRequest.Id;
            var databaseUpdateException = new DbUpdateException();

            var failedHomeRequestStorageException =
                new FailedHomeRequestStorageException(databaseUpdateException);

            var expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedHomeRequestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId)).Throws(databaseUpdateException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(someHomeRequest);

            HomeRequestDependencyException actualHomeRequestDependencyException =
                await Assert.ThrowsAsync<HomeRequestDependencyException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestDependencyException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(someHomeRequest), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnModifyIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest someHomeRequest = randomHomeRequest;
            Guid homeRequestId = someHomeRequest.Id;
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedHomeRequestException =
                new LockedHomeRequestException(dbUpdateConcurrencyException);

            var expectedHomeRequestDependencyValidationException =
                new HomeRequestDependencyValidationException(lockedHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId))
                    .Throws(dbUpdateConcurrencyException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(someHomeRequest);

            HomeRequestDependencyValidationException actualHomeRequestDependencyValidationException =
                await Assert.ThrowsAsync<HomeRequestDependencyValidationException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestDependencyValidationException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(someHomeRequest), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnModifyIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest someHomeRequest = randomHomeRequest;
            Guid homeRequestId = someHomeRequest.Id;
            Exception serviceException = new Exception();

            var failedHomeRequestServiceException =
                new FailedHomeRequestServiceException(serviceException);

            var expectedHomeRequestServiceException =
                new HomeRequestServiceException(failedHomeRequestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId))
                    .Throws(serviceException);

            // when
            ValueTask<HomeRequest> modifyHomeRequestTask =
                this.homeRequestService.ModifyHomeRequestAsync(someHomeRequest);

            HomeRequestServiceException actualHomeRequestServiceException =
                await Assert.ThrowsAsync<HomeRequestServiceException>(
                    modifyHomeRequestTask.AsTask);

            // then
            actualHomeRequestServiceException.Should()
                .BeEquivalentTo(expectedHomeRequestServiceException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestServiceException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(homeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(someHomeRequest), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
