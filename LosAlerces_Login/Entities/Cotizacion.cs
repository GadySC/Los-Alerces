namespace LosAlerces_Login.Entities
{
    public class Cotizacion
    {
        public int ID_Cotizacion { get; set; }
        public int ID_Cliente { get; set; }
        public DateTime QuotationDate { get; set; }
        public string quantityofproduct { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<ProductoCotizacion> ProductosCotizacion { get; set; }
        public virtual ICollection<PersonalCotizacion> PersonalCotizacion { get; set; }
    }
}
