using System;
using System.Collections.Generic;
using System.Linq;
using dominio;

namespace negocio
{
    public class ImagenNegocio
    {
        // atributos
        private readonly List<Imagen> List;
        // constructor
        public ImagenNegocio()
        {
            List = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setQuery("Select Id, IdArticulo, ImagenUrl From IMAGENES");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.IdArticulo = (int)datos.Lector["IdArticulo"];
                    aux.UrlImagen = (string)datos.Lector["ImagenUrl"];
                    List.Add(aux);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        // metodo AgregarImagen
        public void AgregarImagen(int IdArticulo, string UrlImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("Insert into Imagenes (IdArticulo, ImagenUrl) values (@IdArticulo, @ImagenUrl)");
                datos.setearParametro("@IdArticulo", IdArticulo);
                datos.setearParametro("@ImagenUrl", UrlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
            }
        }
        // metodo EliminarImagen
        public void EliminarImagen(int idImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("Delete from Imagenes where id = @id");
                datos.setearParametro("@id", idImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
        public void EliminarImagenesArticulo(int IdArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setQuery("Delete from Imagenes where IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", IdArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
                throw;
            }
        }
        // metodo GetImagenes
        public List<Imagen> GetImagenes(int IdArticulo)
        {
            List<Imagen> images = List.Where(clase => clase.IdArticulo == IdArticulo).ToList();

            if (images == null)
                images.Append(new Imagen(-1, IdArticulo, "https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png"));

            return images;
        }
    }
}
