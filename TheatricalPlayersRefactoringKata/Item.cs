using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata
{
    public class Item
    {
        [XmlIgnore]
        public string PlayName { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }
    }
}
