using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PesonajesClienteAuth.Filters;
using PesonajesClienteAuth.Models;
using PesonajesClienteAuth.Repositories;
using PesonajesClienteAuth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using static PesonajesClienteAuth.ViewModels.PersonajesDropDownListViewModel;

namespace PesonajesClienteAuth.Controllers
{
    public class PersonajesController : Controller
    {
        RepositoryPersonajes repo;

        public PersonajesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {


                List<Personajes> personajes = await
            this.repo.GetPersonajes();

            return View(personajes);

        }

        public async Task<IActionResult> DetallesPersonajes(int empno)
        {
            Personajes emp = await
                this.repo.BuscarPersonaje(empno);
            return View(emp);
        }

        public async Task<IActionResult> ListaPersonajesPorId(int id)
        {
            List<Personajes> personajes = await
            this.repo.GetPersonajesSubordinados(id);

            return View(personajes);

        }
        [EmpleadosAuthorize]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Personajes personaje)
        {
            string token = HttpContext.Session.GetString("TOKEN");
            Personajes emp = await
               this.repo.NuevoPersonaje(personaje,token);
            return RedirectToAction("Index", "Personajes");
        }
        [EmpleadosAuthorize]
        public IActionResult ActualizarPersonajeSerie()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ActualizarPersonajeSerie(Personajes personaje)
        {
            var idPersonaje = personaje.IdPersonaje;
            //Personajes personajeSeleccionado = this.repo.BuscarPersonaje(idPersonaje);
            string token = HttpContext.Session.GetString("TOKEN");
            Personajes emp = await
               this.repo.CambiarPersonaje(personaje,token);
            return RedirectToAction("Index", "Personajes");
        }
    }
}
