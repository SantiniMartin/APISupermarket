namespace ComparadorPreciosAPI.Models
{
public class SupermercadoMarca
{
    public int SupermercadoId { get; set; }
    public Supermercado Supermercado { get; set; }

    public int MarcaId { get; set; }
    public Marca Marca { get; set; }
}
}
