using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BasketballStatsApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
