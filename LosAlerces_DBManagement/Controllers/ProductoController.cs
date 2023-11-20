using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public ProductoController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Productos>>> GetAllProductos()
        {
            try
            {
                var categorias = await _generalDataInterface.GetProductosListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Productos>> CreateProducto([FromBody] ProductosDto productoDto)
        {
            if (productoDto == null)
            {
                return BadRequest("Datos del producto inválidos.");
            }

            try
            {
                var nuevoProducto = new Productos
                {               
                    name = productoDto.name,
                    note = productoDto.note,
                    price = productoDto.price
                };

                await _generalDataInterface.AddProductoAsync(nuevoProducto);
                return CreatedAtAction(nameof(GetProductoById), new { id = nuevoProducto.ID_Productos }, nuevoProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateProducto(int id, [FromBody] ProductosDto productoDto)
        {
            if (productoDto == null)
            {
                return BadRequest("Datos del producto inválidos.");
            }

            try
            {
                var productoExistente = await _generalDataInterface.GetProductoByIdAsync(id);
                if (productoExistente == null)
                {
                    return NotFound($"Producto con ID {id} no encontrado.");
                }

                productoExistente.name = productoDto.name;
                productoExistente.note = productoDto.note;
                productoExistente.price = productoDto.price;

                await _generalDataInterface.UpdateProductoAsync(productoExistente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            try
            {
                await _generalDataInterface.DeleteProductoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filtered/by/{id}")]
        public async Task<ActionResult<Productos>> GetProductoById(int id)
        {
            try
            {
                var producto = await _generalDataInterface.GetProductoByIdAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }
                return Ok(producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
