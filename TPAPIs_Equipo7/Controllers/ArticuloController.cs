using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;

namespace TPAPIs_Equipo7.Controllers
{
    public class ArticuloController : ApiController
    {
        // GET: api/Articulos
        public IEnumerable<Articulo> Get()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            return negocio.listar();
        }

        // GET: api/Articulos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Articulos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Articulos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Articulos/5
        public void Delete(int id)
        {
        }
    }
}
