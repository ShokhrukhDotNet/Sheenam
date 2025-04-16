//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedHomeRequestStorageException =
                new FailedHomeRequestStorageException(sqlException);

            var expectedHomeRequestDependencyException =
                new HomeRequestDependencyException(failedHomeRequestStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomeRequests()).Throws(sqlException);

            // when
            Action retrieveAllHomeRequestsAction = () =>
                this.homeRequestService.RetrieveAllHomeRequests();

            HomeRequestDependencyException actualHomeRequestDependencyException =
                Assert.Throws<HomeRequestDependencyException>(retrieveAllHomeRequestsAction);

            // then
            actualHomeRequestDependencyException.Should()
                .BeEquivalentTo(expectedHomeRequestDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomeRequests(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeRequestDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            // given
            string exceptionMessage = GetRandomString();
            var serverException = new Exception(exceptionMessage);

            var failedHomeRequestServiceException =
                new FailedHomeRequestServiceException(serverException);

            var expectedHomeRequestServiceException =
                new HomeRequestServiceException(failedHomeRequestServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomeRequests()).Throws(serverException);

            // when
            Action retrieveAllHomeRequestActions = () =>
                this.homeRequestService.RetrieveAllHomeRequests();

            HomeRequestServiceException actualHomeRequestServiceException =
                Assert.Throws<HomeRequestServiceException>(retrieveAllHomeRequestActions);

            // then
            actualHomeRequestServiceException.Should()
                .BeEquivalentTo(expectedHomeRequestServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomeRequests(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestServiceException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
