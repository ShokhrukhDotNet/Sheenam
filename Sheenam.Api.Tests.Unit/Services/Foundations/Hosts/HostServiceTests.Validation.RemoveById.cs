﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Hosts;
using Sheenam.Api.Models.Foundations.Hosts.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Hosts
{
    public partial class HostServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRemoveIfIdIsInvalidAndLogItAsync()
        {
            // given
            Guid invalidHostId = Guid.Empty;

            var invalidHostException =
                new InvalidHostException();

            invalidHostException.AddData(
                key: nameof(Host.Id),
                values: "Id is required");

            HostValidationException expectedHostValidationException =
                new HostValidationException(invalidHostException);

            // when
            ValueTask<Host> removeHostById =
                this.hostService.RemoveHostByIdAsync(invalidHostId);

            HostValidationException actualHostValidationException =
                await Assert.ThrowsAsync<HostValidationException>(
                    removeHostById.AsTask);

            // then
            actualHostValidationException.Should()
                .BeEquivalentTo(expectedHostValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHostAsync(It.IsAny<Host>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRemoveHostByIdIsNotFoundAndLogItAsync()
        {
            // given
            Guid inputHostId = Guid.NewGuid();
            Host noHost = null;

            var notFoundHostException =
                new NotFoundHostException(inputHostId);

            var expectedHostValidationException =
                new HostValidationException(notFoundHostException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHostByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noHost);

            // when
            ValueTask<Host> removeHostById =
                this.hostService.RemoveHostByIdAsync(inputHostId);

            var actualHostValidationException =
                await Assert.ThrowsAsync<HostValidationException>(
                    removeHostById.AsTask);

            // then
            actualHostValidationException.Should()
                .BeEquivalentTo(expectedHostValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHostByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHostValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteHostAsync(It.IsAny<Host>()), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
