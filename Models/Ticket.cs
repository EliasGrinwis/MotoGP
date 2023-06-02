using System.ComponentModel.DataAnnotations;

namespace MotoGP.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public int RaceID { get; set; }
        public int Number { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }
        public Boolean Paid { get; set; }

        public Country? Country { get; set; }
        public Race? Race { get; set; }
    }
}
