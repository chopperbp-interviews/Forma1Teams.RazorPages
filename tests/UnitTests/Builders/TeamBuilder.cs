using Forma1Teams.ApplicationCore.Entities;
using System.Collections.Generic;

namespace Forma1Teams.UnitTests.Builders
{
    public class TeamBuilder
    {
        public int TestId => 1;
        public string TestName => "Test Team";
        public int TestYearOfFoundation => 1989;
        public int TestWonChampionships => 3;
        public bool TestPaidEntryFee => true;
        public TeamBuilder()
        {
        }
        public Team WithDefaultValues()
        {
            var team = new Team(TestName, TestYearOfFoundation, TestWonChampionships, TestPaidEntryFee);
            return team;
        }
        public List<Team> GetList()
        {
            return new List<Team> {
                    WithDefaultValues(),
                    new Team("Test Team1", 1989, 0, false),
                    new Team("Test Team2", 1990, 1, true),
                    new Team("Test Team3", 2000, 10, true),
                    };
        }
    }
}
