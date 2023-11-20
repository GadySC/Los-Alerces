using LosAlerces_DBManagement.Models.Dto;

namespace LosAlerces_DBManagement.Entities
{
    public class Cliente
    {
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        // Propiedades anteriores de Contacto ahora en Cliente
        public string ContactoName { get; set; }
        public string ContactoLastname { get; set; }
        public string ContactoEmail { get; set; }
        public string ContactoPhone { get; set; }
    }
}
