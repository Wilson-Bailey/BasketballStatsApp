using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApp.Models
{
    public class UserFavorite
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public int PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
