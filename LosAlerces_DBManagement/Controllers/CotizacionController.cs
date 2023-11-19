using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/cotizacion")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public CotizacionController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Cotizacion>>> GetAllCotizaciones()
        {
            try
            {
                var cotizaciones = await _generalDataInterface.GetAllCotizacionesAsync();
                return Ok(cotizaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cotizacion>> GetCotizacionById(int id)
        {
            try
            {
                var cotizacion = await _generalDataInterface.GetCotizacionByIdAsync(id);
                if (cotizacion == null)
                {
                    return NotFound();
                }
                return Ok(cotizacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Cotizacion>> CreateCotizacion([FromBody] CotizacionDto cotizacionDto)
        {
            try
            {
                var newCotizacion = new Cotizacion
                {
                    ID_Cliente = cotizacionDto.ID_Cliente,
                    name = cotizacionDto.name,
                    quotationDate = cotizacionDto.quotationDate,
                    quantityofproduct = cotizacionDto.quantityofproduct
                    // Aquí debes manejar la asociación con Productos y Personal
                };

                await _generalDataInterface.AddCotizacionAsync(newCotizacion);
                return CreatedAtAction(nameof(GetCotizacionById), new { id = newCotizacion.ID_Cotizacion }, newCotizacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateCotizacion(int id, [FromBody] CotizacionDto cotizacionDto)
        {
            try
            {
                var cotizacionToUpdate = await _generalDataInterface.GetCotizacionByIdAsync(id);
                if (cotizacionToUpdate == null)
                {
                    return NotFound();
                }

                cotizacionToUpdate.ID_Cliente = cotizacionDto.ID_Cliente;
                cotizacionToUpdate.name = cotizacionDto.name;
                cotizacionToUpdate.quotationDate = cotizacionDto.quotationDate;
                cotizacionToUpdate.quantityofproduct = cotizacionDto.quantityofproduct;
                // Manejar actualización de Productos y Personal

                await _generalDataInterface.UpdateCotizacionAsync(cotizacionToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteCotizacion(int id)
        {
            try
            {
                await _generalDataInterface.DeleteCotizacionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
