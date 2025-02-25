using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosInfinite.Models.User
{
    public class User
    {
        [Key] // 📌 Defines Id as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 📌 Makes Id auto-incremental
        public string Id { get; set; }

        // 📌 Personal details
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; } // Identifier (could be ID or Social Security number)

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } // Encrypted password for security purposes

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress] // 📌 Ensures a valid email format
        public string Email { get; set; }

        // 📌 User type, can represent roles such as Admin or Customer
        [Required]
        [MaxLength(50)]
        public string UserType { get; set; }

        // 📌 User role (might be used for authorization purposes)
        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        // ✅ Default constructor
        public User()
        {
            Id = Guid.NewGuid().ToString(); // 📌 Generates a unique ID
            FirstName = string.Empty;
            LastName = string.Empty;
            Cedula = string.Empty;
            Password = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            UserType = string.Empty;
            Role = string.Empty;
        }

        // ✅ Constructor with parameters
        public User(string id, string firstName, string lastName, string cedula, string password, string phone, string email, string userType, string role)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Cedula = cedula;
            Password = password;
            Phone = phone;
            Email = email;
            UserType = userType;
            Role = role;
        }
    }
}
