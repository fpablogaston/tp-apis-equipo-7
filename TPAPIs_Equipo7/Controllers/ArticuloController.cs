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
        public HttpResponseMessage Get()
        {
            try
            {
            ArticuloNegocio negocio = new ArticuloNegocio();
            IEnumerable<Articulo> articulos = negocio.listar();

                if(articulos == null || !articulos.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontraron articulos.");
                }
                
                return Request.CreateResponse(HttpStatusCode.OK, articulos);
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al obtener los articulos. Código de excepción: " + ex);
            }
        }

        // GET: api/Articulos/5
        public HttpResponseMessage Get(int id)
        {

            try
            { 
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> articulos = negocio.listar();  
                
                Articulo articulo = articulos.Find(x => x.IdArticulo == id);

                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Articulo no encontrado.");
                }
    
                return Request.CreateResponse(HttpStatusCode.OK, articulo);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al obtener el articulo. Código de excepción: " + ex);
            }
        }

        // POST: api/Articulos
        public HttpResponseMessage Post([FromBody] ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();


            Marca marca = marcaNegocio.listar().Find(x => x.IdMarca == articulo.IdMarca);
            Categoria categoria = categoriaNegocio.listar().Find(x => x.IdCategoria == articulo.IdCatergoria);




            if (marca == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca inexistente.");
            }

            if (categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Categoria inexistente.");
            }

            try
            {
                Articulo nuevo = new Articulo();

                nuevo.Nombre = articulo.Nombre;
                nuevo.Descripcion = articulo.Descripcion;
                nuevo.ImagenUrl = articulo.ImagenUrl;
                nuevo.Precio = articulo.Precio;
                nuevo.CodigoArticulo = articulo.CodigoArticulo;
                nuevo.Categoria = new Categoria { IdCategoria = articulo.IdCatergoria };
                nuevo.Marca = new Marca { IdMarca = articulo.IdMarca };

                int idArticulo = negocio.agregar(nuevo);
                imagenNegocio.AgregarImagen(idArticulo, articulo.ImagenUrl);

                return Request.CreateResponse(HttpStatusCode.Created, "Articulo agregado exitosamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al agregar el articulo. Código de excepción: " + ex); 
            }
        }

        // PUT: api/Articulos/5
        public HttpResponseMessage Put(int id, [FromBody]ArticuloDto articulo)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            Marca marca = marcaNegocio.listar().Find(x => x.IdMarca == articulo.IdMarca);
            Categoria categoria = categoriaNegocio.listar().Find(x => x.IdCategoria == articulo.IdCatergoria);

            if (marca == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Marca inexistente.");
            }

            if (categoria == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Categoria inexistente.");
            }


            try
            {
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
                imagenNegocio.EliminarImagenesArticulo(id);
                imagenNegocio.AgregarImagen(id, articulo.ImagenUrl);

                return Request.CreateResponse(HttpStatusCode.OK, "Articulo modificado exitosamente.");
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al modificar el articulo. Código de excepción: " + ex);
            }

        }

        // DELETE: api/Articulos/5
        public HttpResponseMessage Delete(int id)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            
            try
            {
                Articulo articulo = negocio.listar().Find(x => x.IdArticulo == id);
                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Articulo no encontrado.");
                }

                negocio.eliminarFisica(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Articulo eliminado exitosamente.");
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al eliminar el articulo. Código de excepción: " + ex);
            }
        }

        List<string> EliminarImagenesRepetidas(List<string> imagenesCargadas, List<Imagen> imagenes)
        {
            List<string> imagenesNuevas = new List<string>();
            List<string> imagenesExistentes = new List<string>();
            foreach (Imagen imagen in imagenes)
            {
                imagenesExistentes.Add(imagen.UrlImagen);
            }

            foreach (string img in imagenesCargadas)
            {
                if (imagenesExistentes.Contains(img))
                {
                    continue;
                }
                else
                {
                    imagenesNuevas.Add(img);
                }
            }

            return imagenesNuevas;
        }

        [HttpPost]
        [Route("api/Articulo/{id}/imagenes")]
        public HttpResponseMessage AgregarImagenes(int id, [FromBody] List<string> imagenes)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo articulo = negocio.listar().Find(x => x.IdArticulo == id);

                if (articulo == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Artículo no encontrado.");
                }

                if (imagenes == null || !imagenes.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Debe enviar al menos una imagen.");
                }


                ImagenNegocio imagenNegocio = new ImagenNegocio();
                imagenes = EliminarImagenesRepetidas(imagenes, imagenNegocio.GetImagenes(id));

                foreach (string imagenUrl in imagenes)
                {
                    imagenNegocio.AgregarImagen(id, imagenUrl);
                }

                return Request.CreateResponse(HttpStatusCode.OK, "Imágenes agregadas exitosamente.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al agregar las imágenes. Código de excepción: " + ex);
            }
        }

    }
}
