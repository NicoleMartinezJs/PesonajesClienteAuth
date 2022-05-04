using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PesonajesClienteAuth.Models;
using PesonajesClienteAuth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Controllers
{
    public class SeriesController : Controller
    {
        RepositoryPersonajes repo;

        public SeriesController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            List<Series> series = await
            this.repo.GetSeries();

            return View(series);

        }

        public async Task<IActionResult> DetallesSeries(int empno)
        {
            Series emp = await
                this.repo.BuscarSerie(empno);
            return View(emp);
        }
    }
}
