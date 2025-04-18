﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System;
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
        public async Task ShouldModifyHomeRequestAsync()
        {
            // given
            HomeRequest randomHomeRequest = CreateRandomHomeRequest();
            HomeRequest inputHomeRequest = randomHomeRequest;
            HomeRequest persistedHomeRequest = inputHomeRequest.DeepClone();
            HomeRequest updatedHomeRequest = inputHomeRequest;
            HomeRequest expectedHomeRequest = updatedHomeRequest.DeepClone();
            Guid InputHomeRequestId = inputHomeRequest.Id;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeRequestByIdAsync(InputHomeRequestId))
                    .ReturnsAsync(persistedHomeRequest);

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest))
                    .ReturnsAsync(updatedHomeRequest);

            // when
            HomeRequest actualHomeRequest =
                await this.homeRequestService
                    .ModifyHomeRequestAsync(inputHomeRequest);

            // then
            actualHomeRequest.Should().BeEquivalentTo(expectedHomeRequest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeRequestByIdAsync(InputHomeRequestId), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeRequestAsync(inputHomeRequest), Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
