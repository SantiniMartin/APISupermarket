namespace ComparadorPreciosAPI.Models
{
    public class CarritoItem
    {
        public int Id { get; set; }
        public int ProductoSupermercadoId { get; set; }
        public ProductoSupermercado ProductoSupermercado { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitarioAlMomento { get; set; }
        public int CarritoId { get; set; }
    }
} 