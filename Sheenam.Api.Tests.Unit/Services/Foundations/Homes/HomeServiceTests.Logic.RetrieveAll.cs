//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Find Comfort and Pease
//==================================================

using FluentAssertions;
using Moq;
using Sheenam.Api.Models.Foundations.Homes;
using System.Linq;

namespace Sheenam.Api.Tests.Unit.Services.Foundations.Homes
{
    public partial class HomeServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllHomes()
        {
            // given
            IQueryable<Home> randomHome = CreateRandomHomes();
            IQueryable<Home> storageHome = randomHome;
            IQueryable<Home> expectedHome = storageHome;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllHomes())
                    .Returns(storageHome);

            // when
            IQueryable<Home> actualHome =
                this.homeService.RetrieveAllHomes();

            // then
            actualHome.Should().BeEquivalentTo(expectedHome);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllHomes(),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
