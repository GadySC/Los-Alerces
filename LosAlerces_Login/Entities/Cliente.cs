namespace LosAlerces_Login.Entities
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public virtual Contactos Contacto { get; set; }
    }
}
