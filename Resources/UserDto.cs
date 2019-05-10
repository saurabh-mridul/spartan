using System.ComponentModel.DataAnnotations;

namespace Spartan.Resources
{
    public class UserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}