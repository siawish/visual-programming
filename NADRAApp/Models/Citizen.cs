using System;
using System.ComponentModel.DataAnnotations;

namespace NADRAApp.Models
{
    public class Citizen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(13)]
        public string CNIC { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string FatherName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = string.Empty;

        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(50)]
        public string Province { get; set; } = string.Empty;

        [StringLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(500)]
        public string? PhotoPath { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [StringLength(50)]
        public string BloodGroup { get; set; } = string.Empty;

        [StringLength(20)]
        public string MaritalStatus { get; set; } = string.Empty;

        [StringLength(100)]
        public string Occupation { get; set; } = string.Empty;
    }
}