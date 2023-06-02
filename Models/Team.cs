using System.ComponentModel.DataAnnotations.Schema;

namespace MotoGP.Models
{
    public class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public ICollection<Rider>? riders { get; set; }
    }
}
