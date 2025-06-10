using System.ComponentModel.DataAnnotations;

namespace AuthDemoYT.Models
{
    public class ImageUpload
    {

        public Guid Id { get; set; }

        [Required]
        public byte[]? Data { get; set; }

        [Required]
        public string? Extension { get; set; }
    }
}
