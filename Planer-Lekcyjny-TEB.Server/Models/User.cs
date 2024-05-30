using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Planer_Lekcyjny_TEB.Server.Models
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

        public DateTime LastLogin { get; set; } = DateTime.UtcNow;

        public bool IsAdmin { get; set; } = false;
    }
}
