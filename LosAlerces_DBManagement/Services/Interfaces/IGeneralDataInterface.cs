using LosAlerces_DBManagement.Entities;

namespace LosAlerces_DBManagement.Services.Interfaces
{
    public interface IGeneralDataInterface
    {
        Task<IEnumerable<Categoria>> GetCategoriasListAsync();
        Task<Categoria> AddCategoriaAsync(Categoria categoria);
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        Task<bool> DeleteCategoriaAsync(int id);
        Task<Categoria> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<Cliente>> GetClienteListAsync();
        Task<IEnumerable<Contactos>> GetContactosListAsync();
        Task<IEnumerable<Cotizacion>> GetCotizacionListAsync();
        Task<IEnumerable<Personal>> GetPersonalListAsync();
        Task<IEnumerable<Productos>> GetProductosListAsync();
    }
}
