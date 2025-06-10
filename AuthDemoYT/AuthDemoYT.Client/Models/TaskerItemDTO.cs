using System.ComponentModel.DataAnnotations;

namespace AuthDemoYT.Client.Models
{
    public class TaskerItemDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Every Task must have a name.")]
        public string? Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
