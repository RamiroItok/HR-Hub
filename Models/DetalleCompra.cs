﻿namespace Models
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}