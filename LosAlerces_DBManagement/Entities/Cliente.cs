namespace LosAlerces_DBManagement.Entities
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string Nombre_Empresa { get; set; }
        public string Direccion { get; set; }
        public virtual ICollection<Contactos> Contactos { get; set; }
    }
}
