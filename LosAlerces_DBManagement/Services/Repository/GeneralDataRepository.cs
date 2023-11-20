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

        public async Task<IEnumerable<ClienteExposicionDto>> GetAllClientesAsync()
        {
            return await _context.Cliente
                .Select(c => new ClienteExposicionDto
                {
                    name = c.name,
                    address = c.address,
                    phone = c.phone,
                    email = c.email,
                    contacto = new ContactoDto
                    {
                        ContactoName = c.ContactoName,
                        ContactoLastname = c.ContactoLastname,
                        ContactoEmail = c.ContactoEmail,
                        ContactoPhone = c.ContactoPhone
                    }
                }).ToListAsync();
        }

        public async Task<ClienteExposicionDto> GetClienteByIdAsync(int clienteId)
        {
            var cliente = await _context.Cliente.FirstOrDefaultAsync(c => c.ID_Cliente == clienteId);

            if (cliente == null) return null;

            return new ClienteExposicionDto
            {
                name = cliente.name,
                address = cliente.address,
                phone = cliente.phone,
                email = cliente.email,
                contacto = new ContactoDto
                {
                    ContactoName = cliente.ContactoName,
                    ContactoLastname = cliente.ContactoLastname,
                    ContactoEmail = cliente.ContactoEmail,
                    ContactoPhone = cliente.ContactoPhone
                }
            };
        }

        public async Task CreateClienteAsync(ClienteDto clienteDto)
        {
            var newCliente = new Cliente
            {
                name = clienteDto.name,
                address = clienteDto.address,
                phone = clienteDto.phone,
                email = clienteDto.email,
                ContactoName = clienteDto.ContactoName,
                ContactoLastname = clienteDto.ContactoLastname,
                ContactoEmail = clienteDto.ContactoEmail,
                ContactoPhone = clienteDto.ContactoPhone
            };

            await _context.Cliente.AddAsync(newCliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(int clienteId, ClienteDto clienteDto)
        {
            var clienteToUpdate = await _context.Cliente.FindAsync(clienteId);

            if (clienteToUpdate != null)
            {
                clienteToUpdate.name = clienteDto.name;
                clienteToUpdate.address = clienteDto.address;
                clienteToUpdate.phone = clienteDto.phone;
                clienteToUpdate.email = clienteDto.email;
                clienteToUpdate.ContactoName = clienteDto.ContactoName;
                clienteToUpdate.ContactoLastname = clienteDto.ContactoLastname;
                clienteToUpdate.ContactoEmail = clienteDto.ContactoEmail;
                clienteToUpdate.ContactoPhone = clienteDto.ContactoPhone;

                _context.Cliente.Update(clienteToUpdate);
                await _context.SaveChangesAsync();
            }
            // Considerar qué hacer si el cliente no se encuentra (opcional)
        }

        public async Task DeleteClienteAsync(int clienteId)
        {
            var clienteToDelete = await _context.Cliente.FindAsync(clienteId);

            if (clienteToDelete != null)
            {
                _context.Cliente.Remove(clienteToDelete);
                await _context.SaveChangesAsync();
            }
            // Considerar qué hacer si el cliente no se encuentra (opcional)
        }

        public async Task UpdateContactoClienteAsync(int clienteId, ContactoDto contactoDto)
        {
            var cliente = await _context.Cliente.FindAsync(clienteId);
            if (cliente != null)
            {
                // Actualizar solo las propiedades de contacto
                cliente.ContactoName = contactoDto.ContactoName;
                cliente.ContactoLastname = contactoDto.ContactoLastname;
                cliente.ContactoEmail = contactoDto.ContactoEmail;
                cliente.ContactoPhone = contactoDto.ContactoPhone;

                _context.Cliente.Update(cliente);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Cliente no encontrado");
            }
        }

        public async Task<IEnumerable<Cotizacion>> GetAllCotizacionesAsync()
        {
            return await _context.Cotizacion
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .ToListAsync();
        }

        public async Task<Cotizacion> GetCotizacionByIdAsync(int id)
        {
            return await _context.Cotizacion
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .FirstOrDefaultAsync(c => c.ID_Cotizacion == id);
        }

        public async Task<Cotizacion> AddCotizacionAsync(CotizacionDto cotizacionDto)
        {
            var newCotizacion = new Cotizacion
            {
                ID_Cliente = cotizacionDto.ID_Cliente,
                name = cotizacionDto.name,
                quotationDate = DateTime.Now,
                quantityofproduct = cotizacionDto.quantityofproduct,
                ProductosCotizacion = cotizacionDto.ProductosIds.Select(pid => new ProductoCotizacion { ID_Producto = pid }).ToList(),
                PersonalCotizacion = cotizacionDto.PersonalIds.Select(pid => new PersonalCotizacion { ID_Personal = pid }).ToList()
            };

            _context.Cotizacion.Add(newCotizacion);
            await _context.SaveChangesAsync();

            return newCotizacion;
        }

        public async Task UpdateCotizacionAsync(int id, CotizacionDto cotizacionDto)
        {
            var cotizacionToUpdate = await _context.Cotizacion
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

                _context.Cotizacion.Update(cotizacionToUpdate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCotizacionAsync(int id)
        {
            var cotizacion = await _context.Cotizacion.FindAsync(id);
            if (cotizacion != null)
            {
                _context.Cotizacion.Remove(cotizacion);
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
