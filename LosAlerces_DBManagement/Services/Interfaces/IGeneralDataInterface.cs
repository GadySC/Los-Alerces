using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;

namespace LosAlerces_DBManagement.Services.Interfaces
{
    public interface IGeneralDataInterface
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int clienteId);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int clienteId);
        Task<IEnumerable<Contactos>> GetAllContactosAsync();
        Task<Contactos> GetContactoByIdAsync(int id);
        Task AddContactoAsync(Contactos contacto);
        Task UpdateContactoAsync(Contactos contacto);
        Task DeleteContactoAsync(int id);
        Task<IEnumerable<Cotizacion>> GetAllCotizacionesAsync();
        Task<Cotizacion> GetCotizacionByIdAsync(int id);
        Task AddCotizacionAsync(CotizacionDto cotizacionDto);
        Task UpdateCotizacionAsync(int id, CotizacionDto cotizacionDto);
        Task DeleteCotizacionAsync(int id);
        Task<IEnumerable<Personal>> GetPersonalListAsync();
        Task<Personal> GetPersonalByIdAsync(int id);
        Task AddPersonalAsync(Personal personal);
        Task UpdatePersonalAsync(Personal personal);
        Task DeletePersonalAsync(int id);
        Task<IEnumerable<Productos>> GetProductosListAsync();
        Task AddProductoAsync(Productos producto);
        Task UpdateProductoAsync(Productos producto);
        Task DeleteProductoAsync(int id);
        Task<Productos> GetProductoByIdAsync(int id);
    }
}
