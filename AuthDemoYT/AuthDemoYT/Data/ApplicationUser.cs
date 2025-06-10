using System.ComponentModel.DataAnnotations;
using AuthDemoYT.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthDemoYT.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        public Guid? ImageId { get; set; }
        public virtual ImageUpload? Image { get; set; }
       
    }

}
