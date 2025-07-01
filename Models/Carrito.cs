namespace ComparadorPreciosAPI.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public ICollection<CarritoItem> Items { get; set; }
    }
} 