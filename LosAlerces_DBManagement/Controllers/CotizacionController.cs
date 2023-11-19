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
                var createdCotizacion = await _generalDataInterface.AddCotizacionAsync(cotizacionDto);
                return CreatedAtAction(nameof(GetCotizacionById), new { id = createdCotizacion.ID_Cotizacion }, createdCotizacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCotizacion(int id, [FromBody] CotizacionDto cotizacionDto)
        {
            try
            {
                await _generalDataInterface.UpdateCotizacionAsync(id, cotizacionDto);
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
