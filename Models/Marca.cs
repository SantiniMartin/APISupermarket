namespace ComparadorPreciosAPI.Models
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Logo { get; set; }

        public ICollection<Producto> Productos { get; set; }
        public ICollection<SupermercadoMarca> SupermercadoMarcas { get; set; }
    }
}
