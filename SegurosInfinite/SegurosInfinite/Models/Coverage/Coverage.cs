using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosInfinite.Models
{
    public class Coverage
    {
        [Key] // 📌 Defines Id as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // 📌 Makes Id auto-incremental
        public int Id { get; set; }

        [Required]
        [MaxLength(255)] // 📌 Limits description length
        public string Descripcion { get; set; }

        [Required]
        public double CostoMinimo { get; set; }

        [Required]
        public double CostoPorcentual { get; set; }

        // 📌 Foreign key for Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        // ✅ Default constructor
        public Coverage()
        {
        }

        // ✅ Constructor with parameters
        public Coverage(int id, string descripcion, double costoMinimo, double costoPorcentual)
        {
            Id = id;
            Descripcion = descripcion;
            CostoMinimo = costoMinimo;
            CostoPorcentual = costoPorcentual;
        }

        // ✅ Constructor with category
        public Coverage(int id, string descripcion, double costoMinimo, double costoPorcentual, Category category)
        {
            Id = id;
            Descripcion = descripcion;
            CostoMinimo = costoMinimo;
            CostoPorcentual = costoPorcentual;
            Category = category;
        }
    }
}
