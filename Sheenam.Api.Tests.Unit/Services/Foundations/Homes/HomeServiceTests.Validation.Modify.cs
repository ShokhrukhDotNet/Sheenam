//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeIsNullAndLogItAsync()
        {
            // given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(nullHome);

            HomeValidationException actualHomeValidationException =
                await Assert.ThrowsAsync<HomeValidationException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeValidationException.Should()
                .BeEquivalentTo(expectedHomeValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(It.IsAny<Home>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeIsInvalidAndLogItAsync(string invalidString)
        {
            // given
            Home invalidHome = new Home
            {
                Address = invalidString
            };

            var invalidHomeException =
                new InvalidHomeException();

            invalidHomeException.AddData(
                key: nameof(Home.HomeId),
                values: "Id is required");

            invalidHomeException.AddData(
                key: nameof(Home.Address),
                values: "Text is required");

            invalidHomeException.AddData(
                key: nameof(Home.AdditionalInfo),
                values: "Text is required");

            invalidHomeException.AddData(
                key: nameof(Home.NumberOfBedrooms),
                values: "Value must be greater than 0");

            invalidHomeException.AddData(
                key: nameof(Home.NumberOfBathrooms),
                values: "Value must be greater than 0");

            invalidHomeException.AddData(
                key: nameof(Home.Area),
                values: "Value must be greater than 0");

            invalidHomeException.AddData(
                key: nameof(Home.Price),
                values: "Value must be greater than 0");

            invalidHomeException.AddData(
                key: nameof(Home.HostId),
                values: "Id is required");

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(invalidHome);

            HomeValidationException actualHomeValidationException =
                await Assert.ThrowsAsync<HomeValidationException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeValidationException.Should()
                .BeEquivalentTo(expectedHomeValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(It.IsAny<Home>()), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnModifyIfHomeDoesNotExistAndLogItAsync()
        {
            // given
            Home randomHome = CreateRandomHome();
            Home nonExistHome = randomHome;
            Home nullHome = null;

            var notFoundHomeException =
                new NotFoundHomeException(nonExistHome.HomeId);

            var expectedHomeValidationException =
                new HomeValidationException(notFoundHomeException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectHomeByIdAsync(nonExistHome.HomeId))
                    .ReturnsAsync(nullHome);

            // when
            ValueTask<Home> modifyHomeTask =
                this.homeService.ModifyHomeAsync(nonExistHome);

            HomeValidationException actualHomeValidationException =
                await Assert.ThrowsAsync<HomeValidationException>(
                    modifyHomeTask.AsTask);

            // then
            actualHomeValidationException.Should()
                .BeEquivalentTo(expectedHomeValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectHomeByIdAsync(nonExistHome.HomeId), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateHomeAsync(nonExistHome), Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
