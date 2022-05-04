using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Models
{
    public class Personajes
    {
        [Key]
        public int IdPersonaje { get; set; }
        public string NombrePersonaje { get; set; }
        public string Imagen { get; set; }
        public int IdSerie { get; set; }
    }
}
