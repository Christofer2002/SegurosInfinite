using SegurosInfinite.Models.User;
using SegurosInfinite.Models;
using System;
using System.Collections.Generic;

namespace SegurosInfinite.Models
{
    public class Insurance
    {
        public int IdPoliza { get; set; }
        public string Placa { get; set; }
        public DateTime FechaInicio { get; set; }
        public string PlazoPago { get; set; }
        public string Auto { get; set; }
        public string Annio { get; set; }
        public decimal CostoTotal { get; set; }
        public Client Client { get; set; }
        public List<Coverage> Coverages { get; set; }
        public int IdPolizaModelo { get; set; }

        // Constructor with params
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
            Client = new Client();
            Coverages = new List<Coverage>();
        }
    }
}