using Ardalis.GuardClauses;

namespace Forma1Teams.ApplicationCore.Entities
{
    public class Team
    {
        public Team(string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee)
        {
            Name = name;
            YearOfFoundation = yearOfFoundation;
            WonChampionships = wonChampionships;
            PaidEntryFee = paidEntryFee;
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int YearOfFoundation { get; private set; }
        public bool PaidEntryFee { get; private set; }
        public int WonChampionships { get; private set; }

        public void UpdateDetails(string name, int yearOfFoundation, int wonChampionships, bool paidEntryFee)
        {
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.NegativeOrZero(yearOfFoundation, nameof(yearOfFoundation));
            Guard.Against.NegativeOrZero(wonChampionships, nameof(wonChampionships));

            Name = name;
            YearOfFoundation = yearOfFoundation;
            WonChampionships = wonChampionships;
            PaidEntryFee = paidEntryFee;
        }
    }
}
