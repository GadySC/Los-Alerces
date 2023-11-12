namespace LosAlerces_Login.Entities
{
    public class Cotizacion
    {
        public int ID_Cotizacion { get; set; }
        public int ID_Cliente { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Personal { get; set; }
        public DateTime Fecha_cotizacion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Total { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Productos Productos { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
