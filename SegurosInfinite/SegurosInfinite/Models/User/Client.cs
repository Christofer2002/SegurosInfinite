using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosInfinite.Models.User
{
    [Serializable]
    public class Client
    {
        [Key] // 📌 Defines Cedula as the primary key
        [Required]
        [MaxLength(20)] // 📌 Ensures Cedula has a reasonable length
        public string Cedula { get; set; }

        [Required]
        [MaxLength(100)] // 📌 Limits name length
        public string Nombre { get; set; }

        // 📌 One-to-One relationship with User
        [ForeignKey("UserId")]
        public User User { get; set; }

        // 📌 One-to-Many relationship with Insurance
        public List<Insurance> Insurances { get; set; }

        // ✅ Default constructor
        public Client()
        {
            Cedula = string.Empty;
            Nombre = string.Empty;
            User = new User();
            Insurances = new List<Insurance>();
        }

        // ✅ Constructor with parameters
        public Client(string cedula, string nombre, User usuario)
        {
            Cedula = cedula;
            Nombre = nombre;
            User = usuario;
            Insurances = new List<Insurance>();
        }

        // ✅ Override Equals for object comparison
        public override bool Equals(object obj)
        {
            if (obj is Client other)
            {
                return Cedula == other.Cedula;
            }
            return false;
        }

        // ✅ Override GetHashCode to maintain consistency with Equals
        public override int GetHashCode()
        {
            return Cedula?.GetHashCode() ?? 0;
        }
    }
}
