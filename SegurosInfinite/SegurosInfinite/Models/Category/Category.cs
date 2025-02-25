using SegurosInfinite.Models;
using System.Collections.Generic;

namespace SegurosInfinite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<Coverage> Coverages { get; set; }

        // Constructor without params
        public Category()
        {
            Coverages = new List<Coverage>();
        }

        // Constructor with params
        public Category(int id, string descripcion, List<Coverage> coberturas)
        {
            Id = id;
            Descripcion = descripcion;
            Coverages = coberturas ?? new List<Coverage>();
        }

        public Category(string descripcion)
        {
            Descripcion = descripcion;
            Coverages = new List<Coverage>();
        }
    }
}
