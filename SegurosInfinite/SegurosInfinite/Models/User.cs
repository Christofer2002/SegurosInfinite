using System;

namespace SegurosInfinite.Models
{
    public class User
    {
        public required string Id { get; set; } // Primary Key

        // Personal details
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public required string Cedula { get; set; } // Identifier (could be ID or Social Security number)
        public required string Password { get; set; } // Encrypted password for security purposes
        public required string Phone { get; set; }
        public required string Email { get; set; }

        // User type, can represent roles such as Admin or Customer
        public required string UserType { get; set; }

        // User role (might be used for authorization purposes)
        public required string Role { get; set; }
    }
}
