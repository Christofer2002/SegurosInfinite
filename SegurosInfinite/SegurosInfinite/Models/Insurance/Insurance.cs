using SegurosInfinite.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SegurosInfinite.Models
{
    public class Insurance
    {
        [Key] // 📌 Define as primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPoliza { get; set; }

        public string Placa { get; set; }
        public DateTime FechaInicio { get; set; }
        public string PlazoPago { get; set; }
        public string Auto { get; set; }
        public string Annio { get; set; }
        public decimal CostoTotal { get; set; }

        // 📌 Relación con Client
        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        // 📌 Relación con Coverages (Muchos a Muchos)
        public List<Coverage> Coverages { get; set; }

        public int IdPolizaModelo { get; set; }

        // Constructor With params
        public Insurance(int idPoliza, string placa, DateTime fechaInicio, string plazoPago, string auto, string annio, decimal costoTotal)
        {
            IdPoliza = idPoliza;
            Placa = placa;
            FechaInicio = fechaInicio;
            PlazoPago = plazoPago;
            Auto = auto;
            Annio = annio;
            CostoTotal = costoTotal;
            Coverages = new List<Coverage>();
            Client = new Client();
        }

        // Constructor without params
        public Insurance()
        {
            Coverages = new List<Coverage>();
            Client = new Client();
        }
    }
}
