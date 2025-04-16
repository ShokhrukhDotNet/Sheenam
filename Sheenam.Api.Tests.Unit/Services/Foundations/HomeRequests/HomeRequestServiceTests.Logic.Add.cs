//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public async Task ShouldAddHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest storageHomeRequest = inputHomeRequest;
            HomeRequest expectedHomeRequest = storageHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertHomeRequestAsync(inputHomeRequest))
                    .ReturnsAsync(storageHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService.AddHomeRequestAsync(inputHomeRequest);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeRequestAsync(inputHomeRequest),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
