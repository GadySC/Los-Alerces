using LosAlerces_DBManagement.Context;
using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LosAlerces_DBManagement.Services.Repository
{
    public class GeneralDataRepository : IGeneralDataInterface
    {
        private readonly LosAlercesDbContext _context;

        public GeneralDataRepository(LosAlercesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasListAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> AddCategoriaAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> UpdateCategoriaAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return false;
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> GetClienteListAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<IEnumerable<Contactos>> GetContactosListAsync()
        {
            return await _context.Contactos.ToListAsync();
        }

        public async Task<IEnumerable<Cotizacion>> GetCotizacionListAsync()
        {
            return await _context.Cotizaciones.ToListAsync();
        }

        public async Task<IEnumerable<Personal>> GetPersonalListAsync()
        {
            return await _context.Personal.ToListAsync();
        }

        public async Task<IEnumerable<Productos>> GetProductosListAsync()
        {
            return await _context.Productos.ToListAsync();
        }
    }
}
