using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Models
{
    public class UsuariosAzure
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
