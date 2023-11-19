namespace LosAlerces_Login.Entities
{
    public class PersonalCotizacion
    {
        public int ID_Cotizacion { get; set; }
        public int ID_Personal { get; set; }
        public virtual Cotizacion Cotizacion { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
