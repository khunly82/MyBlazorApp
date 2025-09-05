using System.ComponentModel.DataAnnotations;

namespace MyBlazorApp.Models
{
    public class LoginForm
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
