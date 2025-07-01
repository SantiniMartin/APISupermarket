namespace ComparadorPreciosAPI.Models
{
    public class ProductoSupermercado
    {
        public int Id { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int SupermercadoId { get; set; }
        public Supermercado Supermercado { get; set; }

        public decimal Precio { get; set; }
        public int? Stock { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string Url { get; set; }
    }
}
