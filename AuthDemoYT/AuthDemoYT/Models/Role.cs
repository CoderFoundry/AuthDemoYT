using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthDemoYT.Models
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin,        
        [Display(Name = "App User")] AppUser
    }
}
