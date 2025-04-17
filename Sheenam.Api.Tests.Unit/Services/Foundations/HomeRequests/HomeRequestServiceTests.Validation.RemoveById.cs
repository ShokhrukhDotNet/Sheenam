//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

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
        public async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidHomeRequestId = Guid.Empty;

            var invalidHomeRequestException =
                new InvalidHomeRequestException();

            invalidHomeRequestException.AddData(
                key: nameof(HomeRequest.Id),
                values: "Id is required");

            HomeRequestValidationException expectedHomeRequestValidationException =
                new HomeRequestValidationException(invalidHomeRequestException);

            // when
            ValueTask<HomeRequest> removeHomeRequestById =
                this.homeRequestService.RemoveHomeRequestByIdAsync(invalidHomeRequestId);

            HomeRequestValidationException actualHomeRequestValidationException =
                await Assert.ThrowsAsync<HomeRequestValidationException>(
                    removeHomeRequestById.AsTask);

            // then
            actualHomeRequestValidationException.Should()
                .BeEquivalentTo(expectedHomeRequestValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(It.IsAny<HomeRequest>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRemoveHomeRequestByIdIsNotFoundAndLogItAsync()
        {
            // given
            Guid inputHomeRequestId = Guid.NewGuid();
            HomeRequest noHomeRequest = null;

            var notFoundHomeRequestException =
                new NotFoundHomeRequestException(inputHomeRequestId);

            var expectedHomeRequestValidationException =
                new HomeRequestValidationException(notFoundHomeRequestException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noHomeRequest);

            // when
            ValueTask<HomeRequest> removeHomeRequestById =
                this.homeRequestService.RemoveHomeRequestByIdAsync(inputHomeRequestId);

            var actualHomeRequestValidationException =
                await Assert.ThrowsAsync<HomeRequestValidationException>(
                    removeHomeRequestById.AsTask);

            // then
            actualHomeRequestValidationException.Should()
                .BeEquivalentTo(expectedHomeRequestValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeRequestValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(It.IsAny<HomeRequest>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
