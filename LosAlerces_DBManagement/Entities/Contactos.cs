namespace LosAlerces_DBManagement.Entities
{
    public class Contactos
    {
        public int ID_Contactos { get; set; }
        public int ID_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
