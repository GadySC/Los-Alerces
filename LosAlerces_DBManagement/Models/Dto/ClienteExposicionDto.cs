namespace LosAlerces_DBManagement.Models.Dto
{
    public class ClienteExposicionDto
    {
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public ContactoDto contacto { get; set; }
    }
}
