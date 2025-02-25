using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosInfinite.Models
{
    public class Category
    {
        [Key] // 📌 Defines Id as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 📌 Makes Id auto-incremental
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // 📌 Limits description length
        public string Descripcion { get; set; }

        // 📌 One-to-Many relationship with Coverage
        public List<Coverage> Coverages { get; set; }

        // ✅ Default constructor
        public Category()
        {
            Coverages = new List<Coverage>();
        }

        // ✅ Constructor with parameters
        public Category(int id, string descripcion, List<Coverage> coverages)
        {
            Id = id;
            Descripcion = descripcion;
            Coverages = coverages ?? new List<Coverage>();
        }

        // ✅ Constructor with only description
        public Category(string descripcion)
        {
            Descripcion = descripcion;
            Coverages = new List<Coverage>();
        }
    }
}
