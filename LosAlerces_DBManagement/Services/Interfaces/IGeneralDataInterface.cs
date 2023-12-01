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
        Task<List<Cliente>> GetAllClientesEntityFormatAsync();
        Task<List<ClienteDto>> GetAllClienteDtoFormatAsync();
        Task UpdateContactoClienteAsync(int clienteId, ContactoDto contactoDto);
        Task<IEnumerable<CotizacionGetAllDto>> GetAllCotizacionesAsync();
        Task<CotizacionGetAllDto> GetCotizacionByIdAsync(int id);
        Task<CotizacionDto> AddCotizacionAsync(CotizacionDto cotizacionDto);
        Task UpdateCotizacionAsync(int id, CotizacionDto cotizacionDto);
        Task DeleteCotizacionAsync(int id);
        Task<List<Cotizacion>> GetAllCotizacionesEntityFormatAsync();
        Task<List<CotizacionExcelDto>> GetAllCotizacionesDtoFormatAsync();
        Task<IEnumerable<Personal>> GetPersonalListAsync();
        Task<Personal> GetPersonalByIdAsync(int id);
        Task AddPersonalAsync(Personal personal);
        Task UpdatePersonalAsync(Personal personal);
        Task DeletePersonalAsync(int id);
        Task<List<Personal>> GetAllPersonalEntityFormatAsync();
        Task<List<PersonalDto>> GetAllPersonalDtoFormatAsync();
        Task<IEnumerable<Productos>> GetProductosListAsync();
        Task AddProductoAsync(Productos producto);
        Task UpdateProductoAsync(Productos producto);
        Task DeleteProductoAsync(int id);
        Task<List<Productos>> GetAllProductosEntityFormatAsync();
        Task<Productos> GetProductoByIdAsync(int id);
        Task<List<ProductosDto>> GetAllProductosDtoFormatAsync();
        byte[] ExportToExcel<T>(List<T> data, string[] columnNames);
    }
}
