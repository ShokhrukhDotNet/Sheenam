//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using System.Linq;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllGuests()
        {
            // given
            IQueryable<Guest> randomGuest = CreateRandomGuests();
            IQueryable<Guest> storageGuest = randomGuest;
            IQueryable<Guest> expectedGuest = storageGuest.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllGuests())
                    .Returns(storageGuest);

            // when
            IQueryable<Guest> actualGuest =
                this.guestService.RetrieveAllGuests();

            // then
            actualGuest.Should().BeEquivalentTo(expectedGuest);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllGuests(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
