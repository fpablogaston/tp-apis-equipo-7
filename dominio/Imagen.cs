namespace dominio
{
    public class Imagen
    {
        // atributos
        public int Id { get; set; }
        public int IdArticulo { get; set; }
        public string UrlImagen { get; set; }
        // constructores
        public Imagen() { }
        public Imagen(int Id_, int IdArticulo_, string UrlImagen_)
        {
            Id = Id_;
            IdArticulo = IdArticulo_;
            UrlImagen = UrlImagen_;
        }
    }
}
