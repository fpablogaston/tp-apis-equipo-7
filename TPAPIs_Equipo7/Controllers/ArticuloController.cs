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
        public void Post([FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
          
                if (!marcaNegocio.MarcaExistente(articulo.IdMarca))
                {
                throw new Exception("Marca inexistente.");
                }

                if (!categoriaNegocio.CategoriaExistente(articulo.IdCatergoria))
                {
                throw new Exception("Categoria inexistente.");
                }


            Articulo nuevo = new Articulo();

            nuevo.Nombre = articulo.Nombre;
            nuevo.Descripcion = articulo.Descripcion;
            nuevo.ImagenUrl = articulo.ImagenUrl;
            nuevo.Precio = articulo.Precio;
            nuevo.CodigoArticulo = articulo.CodigoArticulo;
            nuevo.Categoria = new Categoria { IdCategoria = articulo.IdCatergoria };
            nuevo.Marca = new Marca { IdMarca = articulo.IdMarca };

            negocio.agregar(nuevo);
        }

        // PUT: api/Articulos/5
        public void Put(int id, [FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            if (!marcaNegocio.MarcaExistente(articulo.IdMarca))
            {
                throw new Exception("Marca inexistente.");
            }

            if (!categoriaNegocio.CategoriaExistente(articulo.IdCatergoria))
            {
                throw new Exception("Categoria inexistente.");
            }


            Articulo modificado = new Articulo();

            modificado.Nombre = articulo.Nombre;
            modificado.Descripcion = articulo.Descripcion;
            modificado.ImagenUrl = articulo.ImagenUrl;
            modificado.Precio = articulo.Precio;
            modificado.CodigoArticulo = articulo.CodigoArticulo;
            modificado.Categoria = new Categoria { IdCategoria = articulo.IdCatergoria };
            modificado.Marca = new Marca { IdMarca = articulo.IdMarca };
            modificado.IdArticulo = id;

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
