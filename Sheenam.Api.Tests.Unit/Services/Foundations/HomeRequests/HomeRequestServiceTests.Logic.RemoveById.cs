//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using Force.DeepCloner;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldRemoveHomeRequestById()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputHomeRequestId = randomId;
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest storageHomeRequest = randomHomeRequest;
            HomeRequest expectedInputHomeRequest = storageHomeRequest;
            HomeRequest deletedHomeRequest = expectedInputHomeRequest;
            HomeRequest expectedHomeRequest = deletedHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId))
                    .ReturnsAsync(storageHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHomeRequestAsync(expectedInputHomeRequest))
                    .ReturnsAsync(deletedHomeRequest);

            // when
            HomeRequest actualHomeRequest = await this
                .homeRequestService.RemoveHomeRequestByIdAsync(randomId);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(inputHomeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHomeRequestAsync(expectedInputHomeRequest),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
