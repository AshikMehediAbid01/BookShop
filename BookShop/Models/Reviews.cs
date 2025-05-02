using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace BookShop.Models
{
    public class Reviews
    {
        public int Id { get; set; }

        public string? Review { get; set; }

        public int? Rating { get; set; }

        public DateTime? Created_at { get; set; } = DateTime.Now;
        public string? UserEmail { get; set; }

        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Products? Products { get; set; }

    }
}
