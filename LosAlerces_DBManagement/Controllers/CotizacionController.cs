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

        [HttpPost("create")]
        public async Task<ActionResult<CotizacionDto>> CreateCotizacion([FromBody] CotizacionDto cotizacionDto)
        {
            try
            {
                var createdCotizacionDto = await _generalDataInterface.AddCotizacionAsync(cotizacionDto);

                // Utilizamos el ID de la cotización creada para generar la URL de la nueva cotización
                var actionName = nameof(GetCotizacionById); // Asegúrate de que este es el nombre correcto del método GetCotizacionById
                return CreatedAtAction(actionName, new { id = createdCotizacionDto.ID_Cliente }, createdCotizacionDto);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCotizacion(int id, [FromBody] CotizacionDto cotizacionDto)
        {
            if (cotizacionDto == null)
            {
                return BadRequest("Datos de la cotizacion son invalidos.");
            }

            try
            {
                var cotizacionToUpdate = await _generalDataInterface.GetCotizacionByIdAsync(id);
                if (cotizacionToUpdate == null)
                {
                    return NotFound($"Cotizacion con ID {id} no encontrada.");
                }

                await _generalDataInterface.UpdateCotizacionAsync(id, cotizacionDto);
                return Ok($"Cotizacion con ID {id} actualizada con exito.");
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

        [HttpGet("filtered/by/{id}")]
        public async Task<ActionResult<Cotizacion>> GetCotizacionById(int id)
        {
            try
            {
                var cotizacionDto = await _generalDataInterface.GetCotizacionByIdAsync(id);
                if (cotizacionDto == null)
                {
                    return NotFound($"Cotización con ID {id} no encontrada.");
                }
                return Ok(cotizacionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
