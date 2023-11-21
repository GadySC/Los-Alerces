using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;

namespace LosAlerces_DBManagement.Services.Interfaces
{
    public interface IGeneralDataInterface
    {
        Task<IEnumerable<ClienteExposicionDto>> GetAllClientesAsync();
        Task<ClienteExposicionDto> GetClienteByIdAsync(int clienteId);
        Task CreateClienteAsync(ClienteDto clienteDto);
        Task UpdateClienteAsync(int clienteId, ClienteDto clienteDto);
        Task DeleteClienteAsync(int clienteId);
        Task UpdateContactoClienteAsync(int clienteId, ContactoDto contactoDto);
        Task<IEnumerable<CotizacionGetAllDto>> GetAllCotizacionesAsync();
        Task<CotizacionGetAllDto> GetCotizacionByIdAsync(int id);
        Task<CotizacionDto> AddCotizacionAsync(CotizacionDto cotizacionDto);
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
