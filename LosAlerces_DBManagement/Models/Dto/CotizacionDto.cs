namespace LosAlerces_DBManagement.Models.Dto
{
    public class CotizacionDto
    {
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string quantityofproduct { get; set; }
        public Dictionary<int, int> ProductosIds { get; set; }
        public List<int> PersonalIds { get; set; }
    }
}
