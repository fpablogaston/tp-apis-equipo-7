using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPAPIs_Equipo7.Models
{
    public class ArticuloDto
    {
        public int IdArticulo { get; set; }
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
    }
}