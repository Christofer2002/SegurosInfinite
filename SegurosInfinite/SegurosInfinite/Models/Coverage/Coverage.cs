using System.ComponentModel;
using SegurosInfinite.Models;

namespace SegurosInfinite.Models
{
    public class Coverage
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double CostoMinimo { get; set; }
        public double CostoPorcentual { get; set; }
        public Category Category { get; set; }

        // Constructor sin parámetros
        public Coverage() { }

        // Constructores con parámetros
        public Coverage(int id, string descripcion, double costoMinimo, double costoPorcentual)
        {
            Id = id;
            Descripcion = descripcion;
            CostoMinimo = costoMinimo;
            CostoPorcentual = costoPorcentual;
        }

        public Coverage(string descripcion, double costoMinimo, double costoPorcentual)
        {
            Descripcion = descripcion;
            CostoMinimo = costoMinimo;
            CostoPorcentual = costoPorcentual;
        }

        public Coverage(int id, string descripcion, double costoMinimo, double costoPorcentual, Category categoria)
        {
            Id = id;
            Descripcion = descripcion;
            CostoMinimo = costoMinimo;
            CostoPorcentual = costoPorcentual;
            Category = categoria;
        }
    }
}