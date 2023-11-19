namespace LosAlerces_DBManagement.Models.Dto
{
    public class CotizacionDto
    {
        public int ID_Cliente { get; set; }
        public string name { get; set; }
        public string quantityofproduct { get; set; }
        // ID de Productos y Personal para asociar con la cotización
        public List<int> ProductosIds { get; set; }
        public List<int> PersonalIds { get; set; }
    }
}
