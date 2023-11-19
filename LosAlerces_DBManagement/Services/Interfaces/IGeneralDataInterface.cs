using LosAlerces_DBManagement.Entities;

namespace LosAlerces_DBManagement.Services.Interfaces
{
    public interface IGeneralDataInterface
    {
        Task<IEnumerable<Cliente>> GetClienteListAsync();
        Task<IEnumerable<Contactos>> GetContactosListAsync();
        Task<IEnumerable<Cotizacion>> GetCotizacionListAsync();
        Task<IEnumerable<Personal>> GetPersonalListAsync();
        Task<IEnumerable<Productos>> GetProductosListAsync();
    }
}
