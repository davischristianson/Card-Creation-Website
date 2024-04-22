using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Card_Creation_Website.Models
{
    /// <summary>
    /// Users custom account
    /// </summary>
    public class Account
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }
    }
}
