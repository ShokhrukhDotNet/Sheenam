//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Threading.Tasks;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;
using Sheenam.Api.Models.Foundations.Guests.Exceptions;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsNullAndLogItAsync()
        {
            // given
            Host nullGuest = null;
            var nullGuestException = new NullGuestException();

            var expectedGuestValidationException =
                new GuestValidationException(nullGuestException);

            // when
            ValueTask<Host> addGuestTask =
                this.guestService.AddGuestAsync(nullGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedGuestValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(It.IsAny<Host>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfGuestIsInvalidAndLogItAsync(
            string invalidText)
        {
            // given
            var invalidGuest = new Host
            {
                FirstName = invalidText
            };

            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                key: nameof(Host.Id),
                values: "Id is required");

            invalidGuestException.AddData(
                key: nameof(Host.FirstName),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Host.LastName),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Host.DateOfBirth),
                values: "Date is required");

            invalidGuestException.AddData(
                key: nameof(Host.Email),
                values: "Text is required");

            invalidGuestException.AddData(
                key: nameof(Host.Address),
                values: "Text is required");

            var expectedGuestValidationException =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Host> addGuestTask =
                this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(It.IsAny<Host>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowValidationExceptionOnAddIfGenderIsInvalidAndLogItAsync()
        {
            // given
            Host randomGuest = CreateRandomGuest();
            Host invalidGuest = randomGuest;
            invalidGuest.Gender = GetInvalidEnum<GenderType>();
            var invalidGuestException = new InvalidGuestException();

            invalidGuestException.AddData(
                key: nameof(Host.Gender),
                values: "Value is invalid");

            var expectedGuestValidationException =
                new GuestValidationException(invalidGuestException);

            // when
            ValueTask<Host> addGuestTask =
                this.guestService.AddGuestAsync(invalidGuest);

            // then
            await Assert.ThrowsAsync<GuestValidationException>(() =>
                addGuestTask.AsTask());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedGuestValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertGuestAsync(It.IsAny<Host>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
