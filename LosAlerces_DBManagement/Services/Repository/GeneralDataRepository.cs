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
                    ID_Cliente = c.ID_Cliente,
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
                ID_Cliente = clienteId,
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

        public async Task<IEnumerable<CotizacionDto>> GetAllCotizacionesAsync()
        {
            var cotizaciones = await _context.Cotizacion
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .ToListAsync();

            var cotizacionesDto = cotizaciones.Select(c => new CotizacionDto
            {
                ID_Cotizacion = c.ID_Cotizacion,
                ID_Cliente = c.ID_Cliente,
                name = c.name,
                quantityofproduct = c.quantityofproduct,
                ProductosIds = c.ProductosCotizacion.ToDictionary(p => p.ID_Producto, p => p.Cantidad),
                PersonalIds = c.PersonalCotizacion.Select(p => p.ID_Personal).ToList()
            }).ToList();

            return cotizacionesDto;
        }

        public async Task<CotizacionDto> GetCotizacionByIdAsync(int id)
        {
            var cotizacion = await _context.Cotizacion
                .Include(c => c.ProductosCotizacion)
                .Include(c => c.PersonalCotizacion)
                .FirstOrDefaultAsync(c => c.ID_Cotizacion == id);

            if (cotizacion == null)
            {
                return null;
            }

            var cotizacionDto = new CotizacionDto
            {
                ID_Cotizacion = cotizacion.ID_Cotizacion,
                ID_Cliente = cotizacion.ID_Cliente,
                name = cotizacion.name,
                quantityofproduct = cotizacion.quantityofproduct,
                ProductosIds = cotizacion.ProductosCotizacion.ToDictionary(pc => pc.ID_Producto, pc => pc.Cantidad),
                PersonalIds = cotizacion.PersonalCotizacion.Select(pc => pc.ID_Personal).ToList()
            };

            return cotizacionDto;
        }

        public async Task<CotizacionDto> AddCotizacionAsync(CotizacionDto cotizacionDto)
        {
            var newCotizacion = new Cotizacion
            {
                ID_Cliente = cotizacionDto.ID_Cliente,
                name = cotizacionDto.name,
                quotationDate = DateTime.Now,
                quantityofproduct = cotizacionDto.quantityofproduct,
                ProductosCotizacion = cotizacionDto.ProductosIds.Select(p => new ProductoCotizacion
                {
                    ID_Producto = p.Key,
                    Cantidad = p.Value
                }).ToList(),
                PersonalCotizacion = cotizacionDto.PersonalIds.Select(pid => new PersonalCotizacion { ID_Personal = pid }).ToList()
            };

            _context.Cotizacion.Add(newCotizacion);
            await _context.SaveChangesAsync();

            // Mapeo de la entidad Cotizacion a CotizacionDto para el resultado
            var resultDto = new CotizacionDto
            {
                ID_Cliente = newCotizacion.ID_Cliente,
                name = newCotizacion.name,
                quantityofproduct = newCotizacion.quantityofproduct,
                ProductosIds = newCotizacion.ProductosCotizacion.ToDictionary(pc => pc.ID_Producto, pc => pc.Cantidad),
                PersonalIds = newCotizacion.PersonalCotizacion.Select(pc => pc.ID_Personal).ToList()
            };

            return resultDto;
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

                // Actualización de Productos
                foreach (var productoDto in cotizacionDto.ProductosIds)
                {
                    var producto = cotizacionToUpdate.ProductosCotizacion
                        .FirstOrDefault(p => p.ID_Producto == productoDto.Key);

                    if (producto != null)
                    {
                        producto.Cantidad = productoDto.Value;
                    }
                    else
                    {
                        cotizacionToUpdate.ProductosCotizacion.Add(new ProductoCotizacion
                        {
                            ID_Producto = productoDto.Key,
                            Cantidad = productoDto.Value
                        });
                    }
                }

                // Eliminar productos que ya no están en el DTO
                cotizacionToUpdate.ProductosCotizacion = cotizacionToUpdate.ProductosCotizacion
                    .Where(p => cotizacionDto.ProductosIds.ContainsKey(p.ID_Producto))
                    .ToList();

                // Actualización de Personal
                foreach (var personalId in cotizacionDto.PersonalIds)
                {
                    if (!cotizacionToUpdate.PersonalCotizacion.Any(p => p.ID_Personal == personalId))
                    {
                        cotizacionToUpdate.PersonalCotizacion.Add(new PersonalCotizacion { ID_Personal = personalId });
                    }
                }

                // Eliminar personal que ya no está en el DTO
                cotizacionToUpdate.PersonalCotizacion = cotizacionToUpdate.PersonalCotizacion
                    .Where(p => cotizacionDto.PersonalIds.Contains(p.ID_Personal))
                    .ToList();

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
