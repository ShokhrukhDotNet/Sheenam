//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using Force.DeepCloner;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveHomeByIdAsync()
        {
            // given
            Guid randomHomeId = Guid.NewGuid();
            Guid inputHomeId = randomHomeId;
            Home randomHome = CreateRandomHome();
            Home persistedHome = randomHome;
            Home expectedHome = persistedHome.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(inputHomeId))
                    .ReturnsAsync(persistedHome);

            // when
            Home actualHome = await this
                .homeService.RetrieveHomeByIdAsync(inputHomeId);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(inputHomeId), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
