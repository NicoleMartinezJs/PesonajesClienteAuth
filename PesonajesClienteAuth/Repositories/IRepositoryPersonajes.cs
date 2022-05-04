using PesonajesClienteAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Repositories
{
    public interface IRepositoryPersonajes
    {

        Task<List<Personajes>> GetPersonajes(); 
        Task<Personajes> BuscarPersonaje(int empno);
        Task<UsuariosAzure> PerfilEmpleado(String token);
        Task<UsuariosAzure> BuscarEmpleado(int empno);
        Task<List<Series>> GetSeries();
        Task<Series> BuscarSerie(int empno);
        Task<List<Personajes>> GetPersonajesSubordinados(int id2);
        Task<Personajes> NuevoPersonaje(Personajes personaje, string Token);
        Task<Personajes> CambiarPersonaje(Personajes personaje, string Token);


    }
}
