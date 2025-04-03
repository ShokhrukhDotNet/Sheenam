﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using Moq;
using Sheenam.Api.Models.Foundations.Hosts;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using Force.DeepCloner;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldModifyHostAsync()
        {
            // given
            Host randomHost = CreateRandomHost();
            Host inputHost = randomHost;
            Host persistedHost = inputHost.DeepClone();
            Host updatedHost = inputHost;
            Host expectedHost = updatedHost.DeepClone();
            Guid InputHostId = inputHost.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostByIdAsync(InputHostId))
                    .ReturnsAsync(persistedHost);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHostAsync(inputHost))
                    .ReturnsAsync(updatedHost);

            // when
            Host actualHost =
                await this.hostService
                    .ModifyHostAsync(inputHost);

            // then
            actualHost.Should().BeEquivalentTo(expectedHost);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(InputHostId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHostAsync(inputHost), Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
