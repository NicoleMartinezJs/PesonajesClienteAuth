using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Filters
{
    public class EmpleadosAuthorizeAttribute : AuthorizeAttribute
        , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //SOLO QUEREMOS SABER SI EL EMPLEADO SE HA VALIDADO O NO
            //CUANDO NO ESTE VALIDADO, LO ENVIAMOS A LOGIN
            var user =
                context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                RouteValueDictionary ruta =
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Manage"
                            ,
                            action = "Login"
                        });
                RedirectToRouteResult result =
                    new RedirectToRouteResult(ruta);
                context.Result = result;
            }
        }
    }

}
