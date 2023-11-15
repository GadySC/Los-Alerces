namespace LosAlerces_DBManagement.Entities
{
    public class Productos
    {
        public int ID_Productos { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
