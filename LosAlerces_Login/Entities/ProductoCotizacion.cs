namespace LosAlerces_Login.Entities
{
    public class ProductoCotizacion
    {
        public int ID_Cotizacion { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; } // Nuevo campo para la cantidad

        public virtual Cotizacion Cotizacion { get; set; }
        public virtual Productos Producto { get; set; }
    }
}
