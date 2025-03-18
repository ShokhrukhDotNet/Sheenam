using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Sheenam.Api.Models.Foundations.Guests;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Guests
{
    public partial class GuestServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllGuestAsync()
        {
            // given
            DateTimeOffset randomDateTime = GetRandomDateTimeOffset();
            IQueryable<Guest> randomGuest = CreateRandomGuest(randomDateTime);
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
        }
    }
}
