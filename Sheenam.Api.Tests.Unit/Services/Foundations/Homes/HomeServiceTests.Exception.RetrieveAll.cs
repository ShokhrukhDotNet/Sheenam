//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            // given
            SqlException sqlException = GetSqlError();

            var failedHomeStorageException =
                new FailedHomeStorageException(sqlException);

            var expectedHomeDependencyException =
                new HomeDependencyException(failedHomeStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes()).Throws(sqlException);

            // when
            Action retrieveAllHomesAction = () =>
                this.homeService.RetrieveAllHomes();

            HomeDependencyException actualHomeDependencyException =
                Assert.Throws<HomeDependencyException>(retrieveAllHomesAction);

            // then
            actualHomeDependencyException.Should()
                .BeEquivalentTo(expectedHomeDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedHomeDependencyException))), Times.Once);

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

            var failedHomeServiceException =
                new FailedHomeServiceException(serverException);

            var expectedHomeServiceException =
                new HomeServiceException(failedHomeServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes()).Throws(serverException);

            // when
            Action retrieveAllHomeActions = () =>
                this.homeService.RetrieveAllHomes();

            HomeServiceException actualHomeServiceException =
                Assert.Throws<HomeServiceException>(retrieveAllHomeActions);

            // then
            actualHomeServiceException.Should()
                .BeEquivalentTo(expectedHomeServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(), Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeServiceException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
