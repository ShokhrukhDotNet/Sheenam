﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Moq;
using Sheenam.Api.Models.Foundations.Hosts;
using System.Threading.Tasks;
using System;
using FluentAssertions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldRemoveHostById()
        {
            // given
            Guid randomId = Guid.NewGuid();
            Guid inputHostId = randomId;
            Host randomHost = CreateRandomHost();
            Host storageHost = randomHost;
            Host expectedInputHost = storageHost;
            Host deletedHost = expectedInputHost;
            Host expectedHost = deletedHost;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostByIdAsync(inputHostId))
                    .ReturnsAsync(storageHost);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteHostAsync(expectedInputHost))
                    .ReturnsAsync(deletedHost);

            // when
            Host actualHost = await this
                .hostService.RemoveHostByIdAsync(randomId);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(inputHostId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHostAsync(expectedInputHost),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
