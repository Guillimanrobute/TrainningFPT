using System;
using System.ComponentModel.DataAnnotations;

namespace TrainingFPTCo.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public int RolesId { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be between 1 and 50 characters", MinimumLength = 1)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(150, ErrorMessage = "Password must be between 1 and 150 characters", MinimumLength = 1)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ExtraCode is required")]
        [StringLength(50, ErrorMessage = "ExtraCode must be between 1 and 50 characters", MinimumLength = 1)]
        public string ExtraCode { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, ErrorMessage = "Full Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name must be between 1 and 50 characters", MinimumLength = 1)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birthday is required")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(10, ErrorMessage = "Gender must be either 'Male' or 'Female'", MinimumLength = 4)]
        public string Gender { get; set; }

        [StringLength(150, ErrorMessage = "Education must be less than 150 characters")]
        public string Education { get; set; }

        [StringLength(50, ErrorMessage = "Programming Language must be less than 50 characters")]
        public string ProgrammingLanguage { get; set; }

        [Range(0, 990, ErrorMessage = "TOEIC Score must be between 0 and 990")]
        public int? TOEICScore { get; set; }

        public string Skills { get; set; }

        [StringLength(150, ErrorMessage = "IP Address must be less than 150 characters")]
        public string IPClient { get; set; }

        public DateTime? LastLogin { get; set; }

        public DateTime? LastLogout { get; set; }
        public string Role { get; set; }
    }
}
