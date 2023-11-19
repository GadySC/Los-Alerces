namespace LosAlerces_Login.Entities
{
    public class Contactos
    {
        public int ID_Contactos { get; set; }
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
