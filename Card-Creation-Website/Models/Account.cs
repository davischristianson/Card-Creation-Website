using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Card_Creation_Website.Models
{
    /// <summary>
    /// Users custom account
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Main UserID auto generated
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Username for the user's account
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Password for a user's account
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// User's legal first name
        /// </summary>
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// User's legal last name
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Email for the user's account
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User's legal date of birth
        /// </summary>
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// User's phone number
        /// Should be stored as 1234567890
        /// Displayed as 123-456-7890
        /// </summary>
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// This is a collection of the multiple cards.
        /// Represents the to many side of the relationship.
        /// </summary>
        public ICollection<Card> Cards { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}