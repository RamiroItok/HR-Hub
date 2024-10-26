namespace Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdEmpresa { get; set; }
        public byte[] Imagen { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int DVH { get; set; }
    }
}