using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata
{
    public class Statement
    {
        public string Customer { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
    }
}
