namespace LosAlerces_DBManagement.Entities
{
    public class ProductoCotizacion
    {
        public int ID_Cotizacion { get; set; }
        public int ID_Producto { get; set; }
        public virtual Cotizacion Cotizacion { get; set; }
        public virtual Productos Producto { get; set; }
    }
}
