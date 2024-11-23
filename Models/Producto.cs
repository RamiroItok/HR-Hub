namespace Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Empresa Empresa { get; set; }
        public byte[] Imagen { get; set; }
        public string Descripcion { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
    }
}