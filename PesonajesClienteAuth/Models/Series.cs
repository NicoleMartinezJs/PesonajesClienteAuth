using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Models
{
    public class Series
    {
        [Key]
        public int IdSerie { get; set; }
        public string NombreSerie { get; set; }
        public string Imagen { get; set; }
        public double Puntuacion { get; set; }
        public int Fecha { get; set; }
    }
}
