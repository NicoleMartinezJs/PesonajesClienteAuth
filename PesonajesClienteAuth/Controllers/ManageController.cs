using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PesonajesClienteAuth.Models;
using PesonajesClienteAuth.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Controllers
{
    public class ManageController : Controller
    {
        RepositoryPersonajes repo;
 
        public ManageController(RepositoryPersonajes repo)
        {
            this.repo = repo;
        }
 
        public IActionResult Login()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Login(String username, String password)
        {
            //PARA VALIDAR USUARIOS, UTILIZAMOS EL TOKEN
            //SI EL TOKEN ES NULL, CREDENCIALES INCORRECTAS
            String token = await this.repo.GetToken(username, password);
            if (token == null)
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
            else
            {
                //RECUPERAR AL USUARIO QUE SE HA VALIDADO
                UsuariosAzure empleado = await this.repo.PerfilEmpleado(token);
                //HABILITAMOS LA SEGURIDAD DE MVC CORE CON CLAIMS
                ClaimsIdentity identity =
                    new ClaimsIdentity(CookieAuthenticationDefaults
                    .AuthenticationScheme, ClaimTypes.Name
                    , ClaimTypes.Role);
                //ALMACENAMOS EL NUMERO DE EMPLEADO
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier
                    , empleado.Password.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name
                    , empleado.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role
                    , empleado.IdUsuario.ToString()));
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme
                    , principal, new AuthenticationProperties
                    {
                        IsPersistent = true, ExpiresUtc = 
                        DateTime.Now.AddMinutes(60)
                    });
                //DEBEMOS ALMACENAR EL TOKEN UNA VEZ QUE EL USUARIO YA EXISTE
                HttpContext.Session.SetString("TOKEN", token.ToString());
                return RedirectToAction("Index", "Empleados");
            }
        }
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            if (HttpContext.Session.GetString("TOKEN") != null)
            {
                //HttpContext.Session.SetString("TOKEN", String.Empty);
                HttpContext.Session.Remove("TOKEN");
            }
            return RedirectToAction("Index", "Home");
        }


    }
}


