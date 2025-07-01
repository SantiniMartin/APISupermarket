namespace ComparadorPreciosAPI.Models
{
public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Imagen { get; set; }
    public string Descripcion { get; set; }
    public string UnidadMedida { get; set; }
    public string EAN { get; set; }

    // FK
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }

    // Relación con Marca
    public int MarcaId { get; set; }
    public Marca Marca { get; set; }

    public ICollection<ProductoSupermercado> ProductoSupermercados { get; set; }
}
}
