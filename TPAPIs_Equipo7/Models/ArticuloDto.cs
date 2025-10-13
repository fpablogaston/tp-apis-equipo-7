using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPAPIs_Equipo7.Models
{
    public class ArticuloDto
    {
        public string CodigoArticulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdMarca { get; set; }
        public int IdCatergoria { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }
    }
}