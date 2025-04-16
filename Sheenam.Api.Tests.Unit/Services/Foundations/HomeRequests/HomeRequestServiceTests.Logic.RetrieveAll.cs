//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.HomeRequests;
using System.Linq;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.HomeRequests
{
    public partial class HomeRequestServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllHomeRequests()
        {
            // given
            IQueryable<HomeRequest> randomHomeRequest = CreateRandomHomeRequests();
            IQueryable<HomeRequest> storageHomeRequest = randomHomeRequest;
            IQueryable<HomeRequest> expectedHomeRequest = storageHomeRequest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomeRequests())
                    .Returns(storageHomeRequest);

            // when
            IQueryable<HomeRequest> actualHomeRequest =
                this.homeRequestService.RetrieveAllHomeRequests();

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomeRequests(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
