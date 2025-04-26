using System.ComponentModel.DataAnnotations;

namespace BasketballStatsApp.Models
{
    public class Player
    {
        public int PlayerId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Position { get; set; }

        [Range(18, 50)]
        public int Age { get; set; }

        [Range(0, 100)]
        public double PointsPerGame { get; set; }

        [Range(0, 100)]
        public double AssistsPerGame { get; set; }

        [Range(0, 100)]
        public double ReboundsPerGame { get; set; }

        [Required]
        public int TeamId { get; set; }

        public Team? Team { get; set; }

        public ICollection<UserFavorite> UserFavorites { get; set; } = new List<UserFavorite>();

        public string? ImageFileName { get; set; }
    }
}
