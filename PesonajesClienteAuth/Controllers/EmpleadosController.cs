using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PesonajesClienteAuth.Filters;
using PesonajesClienteAuth.Models;
using PesonajesClienteAuth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Controllers
{
    public class EmpleadosController : Controller
    {
        RepositoryPersonajes repo;

        public EmpleadosController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }
        [EmpleadosAuthorize]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("TOKEN");
            UsuariosAzure empleado = await
                this.repo.PerfilEmpleado(token);
            return View(empleado);
        }
        [EmpleadosAuthorize]
        public async Task<IActionResult> DetallesEmpleado(int empno)
        {
            UsuariosAzure emp = await
                this.repo.BuscarEmpleado(empno);
            return View(emp);
        }

    }
}
