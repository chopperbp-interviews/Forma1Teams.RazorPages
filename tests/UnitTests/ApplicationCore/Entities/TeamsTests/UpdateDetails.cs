using Forma1Teams.ApplicationCore.Entities;
using Forma1Teams.UnitTests.Builders;
using System;
using Xunit;

namespace Forma1Teams.UnitTests.ApplicationCore.Entities.TeamsTests
{
    public class UpdateDetails
    {
        private readonly Team testTeam;
        private readonly TeamBuilder teamBuilder = new TeamBuilder();
        public UpdateDetails()
        {
            
            testTeam = teamBuilder.WithDefaultValues();
        }

        [Fact]
        public void ThrowsArgumentExceptionGivenEmptyName()
        {
            string newValue = "";
            Assert.Throws<ArgumentException>(() => testTeam.UpdateDetails(newValue, teamBuilder.TestYearOfFoundation, teamBuilder.TestWonChampionships, teamBuilder.TestPaidEntryFee));
        }

        [Fact]
        public void ThrowsArgumentNullExceptionGivenNullName()
        {
            Assert.Throws<ArgumentNullException>(() => testTeam.UpdateDetails(null , teamBuilder.TestYearOfFoundation, teamBuilder.TestWonChampionships, teamBuilder.TestPaidEntryFee));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsArgumentExceptionGivenNonPositiveYearOfFoundation(int newYearOfFoundation)
        {
            Assert.Throws<ArgumentException>(() => testTeam.UpdateDetails(teamBuilder.TestName, newYearOfFoundation, teamBuilder.TestWonChampionships, teamBuilder.TestPaidEntryFee));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsArgumentExceptionGivenNonPositiveWonChampionships(int newWonChampionships)
        {
            Assert.Throws<ArgumentException>(() => testTeam.UpdateDetails(teamBuilder.TestName, teamBuilder.TestYearOfFoundation, newWonChampionships, teamBuilder.TestPaidEntryFee));
        }
    }
}
