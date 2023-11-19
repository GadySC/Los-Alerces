using LosAlerces_DBManagement.Entities;

namespace LosAlerces_DBManagement.Services.Interfaces
{
    public interface IGeneralDataInterface
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int clienteId);
        Task CreateClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task DeleteClienteAsync(int clienteId);
        Task<IEnumerable<Contactos>> GetContactosListAsync();
        Task<IEnumerable<Cotizacion>> GetCotizacionListAsync();
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
