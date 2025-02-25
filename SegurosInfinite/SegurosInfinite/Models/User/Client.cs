using System;
using System.Collections.Generic;
using SegurosInfinite.Models;
namespace SegurosInfinite.Models.User
{
    [Serializable]
    public class Client
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public User User { get; set; }
        public List<Insurance> Insurances { get; set; }

        // Constructor without params
        public Client() : this("", "", new User
        {
            Id = "",
            FirstName = "",
            LastName = "",
            Cedula = "",
            Password = "",
            Phone = "",
            Email = "",
            UserType = "",
            Role = ""
        })
        { }


        // Constructor with params
        public Client(string cedula, string nombre, User usuario)
        {
            Cedula = cedula;
            Nombre = nombre;
            User = usuario;
            Insurances = new List<Insurance>();
        }

        // Método Equals para comparación de objetos
        public override bool Equals(object obj)
        {
            if (obj is Client other)
            {
                return Cedula == other.Cedula;
            }
            return false;
        }

        // Sobreescribir GetHashCode para mantener coherencia con Equals
        public override int GetHashCode()
        {
            return Cedula?.GetHashCode() ?? 0;
        }
    }
}
