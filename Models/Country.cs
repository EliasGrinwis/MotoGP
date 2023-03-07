using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CountryID { get; set; }
        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Rider> Riders { get; set; }

    }
}
