using LosAlerces_DBManagement.Context;
using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
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

        public async Task<IEnumerable<Contactos>> GetAllContactosAsync()
        {
            return await _context.Contactos.ToListAsync();
        }

        public async Task<Contactos> GetContactoByIdAsync(int id)
        {
            return await _context.Contactos.FindAsync(id);
        }

        public async Task AddContactoAsync(Contactos contacto)
        {
            await _context.Contactos.AddAsync(contacto);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactoAsync(Contactos contacto)
        {
            _context.Contactos.Update(contacto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactoAsync(int id)
        {
            var contacto = await _context.Contactos.FindAsync(id);
            if (contacto != null)
            {
                _context.Contactos.Remove(contacto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionesAsync()
        {
            return await _context.Cotizaciones
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .ToListAsync();
        }

        public async Task<Cotizacion> GetCotizacionByIdAsync(int id)
        {
            return await _context.Cotizaciones
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .FirstOrDefaultAsync(c => c.ID_Cotizacion == id);
        }

        public async Task AddCotizacionAsync(CotizacionDto cotizacionDto)
        {
            var newCotizacion = new Cotizacion
            {
                ID_Cliente = cotizacionDto.ID_Cliente,
                name = cotizacionDto.name,
                quotationDate = DateTime.Now,
                quantityofproduct = cotizacionDto.quantityofproduct,
                ProductosCotizacion = new List<ProductoCotizacion>(),
                PersonalCotizacion = new List<PersonalCotizacion>()
            };

            foreach (var productoId in cotizacionDto.ProductosIds)
            {
                newCotizacion.ProductosCotizacion.Add(new ProductoCotizacion { ID_Producto = productoId });
            }

            foreach (var personalId in cotizacionDto.PersonalIds)
            {
                newCotizacion.PersonalCotizacion.Add(new PersonalCotizacion { ID_Personal = personalId });
            }

            _context.Cotizaciones.Add(newCotizacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCotizacionAsync(int id, CotizacionDto cotizacionDto)
        {
            var cotizacionToUpdate = await _context.Cotizaciones
        .Include(c => c.ProductosCotizacion)
        .Include(c => c.PersonalCotizacion)
        .FirstOrDefaultAsync(c => c.ID_Cotizacion == id);

            if (cotizacionToUpdate != null)
            {
                cotizacionToUpdate.ID_Cliente = cotizacionDto.ID_Cliente;
                cotizacionToUpdate.name = cotizacionDto.name;
                cotizacionToUpdate.quotationDate = cotizacionToUpdate.quotationDate;
                cotizacionToUpdate.quantityofproduct = cotizacionDto.quantityofproduct;

                // Actualizar asociaciones de productos
                var currentProductIds = cotizacionToUpdate.ProductosCotizacion.Select(p => p.ID_Producto).ToList();
                var productIdsToAdd = cotizacionDto.ProductosIds.Except(currentProductIds);
                var productIdsToRemove = currentProductIds.Except(cotizacionDto.ProductosIds);

                foreach (var productId in productIdsToAdd)
                {
                    cotizacionToUpdate.ProductosCotizacion.Add(new ProductoCotizacion { ID_Producto = productId });
                }

                foreach (var productId in productIdsToRemove)
                {
                    var productToRemove = cotizacionToUpdate.ProductosCotizacion.FirstOrDefault(p => p.ID_Producto == productId);
                    if (productToRemove != null)
                    {
                        cotizacionToUpdate.ProductosCotizacion.Remove(productToRemove);
                    }
                }

                // Actualizar asociaciones de personal
                var currentPersonalIds = cotizacionToUpdate.PersonalCotizacion.Select(p => p.ID_Personal).ToList();
                var personalIdsToAdd = cotizacionDto.PersonalIds.Except(currentPersonalIds);
                var personalIdsToRemove = currentPersonalIds.Except(cotizacionDto.PersonalIds);

                foreach (var personalId in personalIdsToAdd)
                {
                    cotizacionToUpdate.PersonalCotizacion.Add(new PersonalCotizacion { ID_Personal = personalId });
                }

                foreach (var personalId in personalIdsToRemove)
                {
                    var personalToRemove = cotizacionToUpdate.PersonalCotizacion.FirstOrDefault(p => p.ID_Personal == personalId);
                    if (personalToRemove != null)
                    {
                        cotizacionToUpdate.PersonalCotizacion.Remove(personalToRemove);
                    }
                }

                _context.Cotizaciones.Update(cotizacionToUpdate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCotizacionAsync(int id)
        {
            var cotizacion = await _context.Cotizaciones.FindAsync(id);
            if (cotizacion != null)
            {
                _context.Cotizaciones.Remove(cotizacion);
                await _context.SaveChangesAsync();
            }
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
