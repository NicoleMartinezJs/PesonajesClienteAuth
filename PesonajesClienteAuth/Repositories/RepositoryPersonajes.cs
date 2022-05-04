using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PesonajesClienteAuth.Models;
using PesonajesClienteAuth.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PesonajesClienteAuth.Repositories
{
    public class RepositoryPersonajes: IRepositoryPersonajes
    {
        //static HttpClient client = new HttpClient();
        private String url;
        private MediaTypeWithQualityHeaderValue header;

        public RepositoryPersonajes()
        {
            
            this.url = "https://apipersonajescore0auth20220502204801.azurewebsites.net/";
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODO PARA VALIDAR USUARIOS NO DEVUELVE UN EMPLEADO
        //DEVOLVERA UN TOKEN...
        //EL SERVICIO NOS DIRA SI EXISTE O NO EXISTE
        public async Task<String> GetToken(String username, String password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                //CREAMOS EL MODELO LOGIN PARA EL API
                LoginModel login = new LoginModel();
                login.UserName = username;
                login.Password = password;
                //CONVERTIMOS EL LOGIN A JSON PARA EL SERVICIO API
                String json = JsonConvert.SerializeObject(login);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                String request = "Auth/Login";
                HttpResponseMessage response = await
                    client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    String data = await
                        response.Content.ReadAsStringAsync();
                    JObject jobject = JObject.Parse(data);
                    String token = jobject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        //METODO PARA RESOLVER LAS PETICIONES API <T>
        //SIN SEGURIDAD (TOKEN)
        private async Task<T> CallApi<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await
                        response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }

        //METODO PARA RESOLVER LAS PETICIONES API <T>
        //CON SEGURIDAD (TOKEN)
        private async Task<T> CallApi<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization"
                    , "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await
                        response.Content.ReadAsAsync<T>();
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }

        //METODOS PARA LAS PETICIONES API Y NUESTRA APP CLIENTE MVC
        //SIN SEGURIDAD
        public async Task<UsuariosAzure> BuscarEmpleado(int empno)
        {
            UsuariosAzure emp = await
                this.CallApi<UsuariosAzure>("api/empleados/" + empno);
            return emp;
        }

        //CON SEGURIDAD
        public async Task<UsuariosAzure> PerfilEmpleado(String token)
        {
            UsuariosAzure emp = await
                this.CallApi<UsuariosAzure>("api/empleados/perfilempleado", token);
            return emp;
        }

        public async Task<List<Personajes>> GetPersonajes()
        {
            List<Personajes> personajes = await this.CallApi<List<Personajes>>("api/personajes");
            return personajes;
        }
        public async Task<Personajes> BuscarPersonaje(int empno)
        {
            //Sin Seguridad
            Personajes emp = await this.CallApi<Personajes>("api/personajes/" + empno);
            return emp;
        } 
        public async Task<List<Series>> GetSeries()
        {
            List<Series> series = await this.CallApi<List<Series>>("api/series");
            return series;
        }
        public async Task<Series> BuscarSerie(int empno)
        {
            //Sin Seguridad
            Series emp = await this.CallApi<Series>("api/series/" + empno);
            return emp;
        }
        public async Task<List<Personajes>> GetPersonajesSubordinados(int id2)
        {
            List<Personajes> personajes = await this.CallApi<List<Personajes>>("api/personajes/serie/" + id2);
            return personajes;
        }
        public async Task<Personajes> NuevoPersonaje(Personajes personaje, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization"
                    , "bearer " + token);
                HttpResponseMessage response =
                    await client.PostAsJsonAsync("api/personajes", personaje);
            }
            //    using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(this.url);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(header);
            //    HttpResponseMessage response =
            //        await client.PostAsJsonAsync("api/personajes", personaje);
            //}
                //HttpResponseMessage response = await client.PostAsJsonAsync(
                //"https://localhost:44347/api/personajes", personaje);
            //response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return null;
        }
        public async Task<Personajes> CambiarPersonaje(Personajes personaje, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                client.DefaultRequestHeaders.Add("Authorization"
                    , "bearer " + token);
                HttpResponseMessage response =
                    await client.PutAsJsonAsync("api/personajes/{personaje.IdPersonaje}", personaje);
            }
            //using (HttpClient client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(this.url);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(header);
            //    HttpResponseMessage response =
            //        await client.PutAsJsonAsync("api/personajes/{personaje.IdPersonaje}", personaje);
            //}
            return null;

            //HttpResponseMessage response = await client.PutAsJsonAsync(
            //    $"https://localhost:44347/api/personajes/{personaje.IdPersonaje}", personaje);
            //return null;
        }

    }
}


