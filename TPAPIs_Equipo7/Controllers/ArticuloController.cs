using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dominio;
using negocio;
using TPAPIs_Equipo7.Models;

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
        public Articulo Get(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> articulos = negocio.listar();            
            return articulos.Find(x => x.IdArticulo == id);
        }

        // POST: api/Articulos
        public void Post([FromBody]ArticuloDto value)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo nuevo = new Articulo();

            nuevo.Nombre = value.Nombre;
            nuevo.Descripcion = value.Descripcion;
            nuevo.ImagenUrl = value.ImagenUrl;
            nuevo.Precio = value.Precio;
            nuevo.CodigoArticulo = value.CodigoArticulo;
            nuevo.Categoria = new Categoria { IdCategoria = value.IdCatergoria };
            nuevo.Marca = new Marca { IdMarca = value.IdMarca };

            // TODO: validar si existe el IdMarca y el IdCategoria antes de agregar
            negocio.agregar(nuevo);
        }

        // PUT: api/Articulos/5
        public void Put(int id, [FromBody]Articulo articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo modificado = new Articulo();

            modificado.Nombre = articulo.Nombre;
            modificado.Descripcion = articulo.Descripcion;
            modificado.ImagenUrl = articulo.ImagenUrl;
            modificado.Precio = articulo.Precio;
            modificado.CodigoArticulo = articulo.CodigoArticulo;
            modificado.Categoria = articulo.Categoria;
            modificado.IdArticulo = id;
            modificado.Marca = articulo.Marca;

            negocio.modificar(modificado);
        }

        // DELETE: api/Articulos/5
        public void Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            negocio.eliminarFisica(id);
        }
    }
}
