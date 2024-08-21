using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Domain.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [XmlIgnore]
        public string PlayName { get; set; }
        public decimal AmountOwed { get; set; }
        public int EarnedCredits { get; set; }
        public int Seats { get; set; }

        public Item()
        {

        }

        public Item(string playName, decimal amountOwed, int earnedCredits, int seats)
        {
            this.PlayName = playName;
            this.AmountOwed = amountOwed;
            this.EarnedCredits = earnedCredits;
            this.Seats = seats;
        }
    }
}
