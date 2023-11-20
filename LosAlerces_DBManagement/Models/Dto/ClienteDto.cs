namespace LosAlerces_DBManagement.Models.Dto
{
    public class ClienteDto
    {
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        // Propiedades de Contacto aquí
        public string ContactoName { get; set; }
        public string ContactoLastname { get; set; }
        public string ContactoEmail { get; set; }
        public string ContactoPhone { get; set; }
    }
}
