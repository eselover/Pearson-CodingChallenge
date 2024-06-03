using System.ComponentModel.DataAnnotations;

namespace Pearson_CodingChallenge.Models
{
    public class DataFile
    {
        [Required]
        [DataType(DataType.Upload)]
        public IFormFile? ImportFile { get; set; }

        public string? Message { get; set; }
    }
}
