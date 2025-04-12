//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldModifyHomeAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home inputHome = randomHome;
            Home persistedHome = inputHome.DeepClone();
            Home updatedHome = inputHome;
            Home expectedHome = updatedHome.DeepClone();
            Guid InputHomeId = inputHome.HomeId;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(InputHomeId))
                    .ReturnsAsync(persistedHome);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeAsync(inputHome))
                    .ReturnsAsync(updatedHome);

            // when
            Home actualHome =
                await this.homeService
                    .ModifyHomeAsync(inputHome);

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(InputHomeId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(inputHome), Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
