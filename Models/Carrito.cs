namespace Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public Usuario Usuario { get; set; }
        public int Cantidad { get; set; }

        public int IdProducto => Producto?.Id ?? 0;
        public decimal Subtotal => Cantidad * Producto.PrecioUnitario.GetValueOrDefault(0);
    }
}