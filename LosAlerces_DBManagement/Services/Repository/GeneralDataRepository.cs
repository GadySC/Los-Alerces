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

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int clienteId)
        {
            return await _context.Clientes.FindAsync(clienteId);
        }

        public async Task CreateClienteAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int clienteId)
        {
            var cliente = await GetClienteByIdAsync(clienteId);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
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

        public async Task<Personal> GetPersonalByIdAsync(int id)
        {
            return await _context.Personal.FindAsync(id);
        }

        public async Task AddPersonalAsync(Personal personal)
        {
            await _context.Personal.AddAsync(personal);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonalAsync(Personal personal)
        {
            _context.Personal.Update(personal);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonalAsync(int id)
        {
            var personalToDelete = await _context.Personal.FindAsync(id);
            if (personalToDelete != null)
            {
                _context.Personal.Remove(personalToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Productos>> GetProductosListAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task AddProductoAsync(Productos producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductoAsync(Productos producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Productos> GetProductoByIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }
    }
}
