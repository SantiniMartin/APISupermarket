namespace ComparadorPreciosAPI.Models
{
	public class Supermercado
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Logo { get; set; }
		public string HorarioAtencion { get; set; }
		public string Direccion { get; set; }
		public string Ubicacion { get; set; } // Ejemplo: "-34.6037,-58.3816" (latitud,longitud)

		// Relaciones
		public ICollection<ProductoSupermercado> ListaProductos { get; set; }
		public ICollection<SupermercadoMarca> Marcas { get; set; }
	}
}
