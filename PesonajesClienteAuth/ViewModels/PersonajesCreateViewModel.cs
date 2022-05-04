using PesonajesClienteAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.ViewModels
{
    public class PersonajesCreateViewModel
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public int IdSerie { get; set; }
    }
}
