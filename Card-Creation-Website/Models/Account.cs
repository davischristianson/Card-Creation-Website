﻿using System.ComponentModel.DataAnnotations;
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
        public string Username { get; set; }

        /// <summary>
        /// Password for a user's account
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email for the user's account
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// User's date of birth
        /// </summary>
        [Required]
        public DateOnly DateOfBirth { get; set; }

        /// <summary>
        /// User's phone numberss
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
