using System.ComponentModel.DataAnnotations;

namespace Pearson_CodingChallenge.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string? CustomerId { get; set; }
        [Required]
        public string? StudyGuideId { get; set; }
        public bool IsFulfilled { get; set; }
        public DateOnly DateFulfilled { get; set; }
    }
}
