using System.ComponentModel.DataAnnotations;

namespace Pearson_CodingChallenge.Models
{
    public class Customer
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
    }
}
