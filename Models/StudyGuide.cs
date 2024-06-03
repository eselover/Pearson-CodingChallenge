using System.ComponentModel.DataAnnotations;

namespace Pearson_CodingChallenge.Models
{
    public class StudyGuide
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public double Price { get; set; }
    }
}