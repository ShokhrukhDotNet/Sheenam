//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Threading.Tasks;
using Moq;
using Sheenam.Api.Models.Foundations.Homes.Exceptions;
using Sheenam.Api.Models.Foundations.Homes;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsNullAndLogItAsync()
        {
            // given
            Home nullHome = null;
            var nullHomeException = new NullHomeException();

            var expectedHomeValidationException =
                new HomeValidationException(nullHomeException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(nullHome);

            // then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedHomeValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfHomeIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidHome = new Home
            {
                Address = invalidText
            };

            var invalidHomeException = new InvalidHomeException();

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

            var expectedHomeValidationException =
                new HomeValidationException(invalidHomeException);

            // when
            ValueTask<Home> addHomeTask =
                this.homeService.AddHomeAsync(invalidHome);

            // then
            await Assert.ThrowsAsync<HomeValidationException>(() =>
                addHomeTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedHomeValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertHomeAsync(It.IsAny<Home>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
