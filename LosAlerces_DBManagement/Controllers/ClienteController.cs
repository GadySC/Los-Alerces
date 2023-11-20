using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public ClienteController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAll()
        {
            try
            {
                var clientes = await _generalDataInterface.GetAllClientesAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetById(int id)
        {
            try
            {
                var cliente = await _generalDataInterface.GetClienteByIdAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Cliente>> Create([FromBody] ClienteDto clienteDto)
        {
            try
            {
                await _generalDataInterface.CreateClienteAsync(clienteDto);
                // Puedes devolver la entidad creada, aunque aquí se devuelve solo un mensaje de éxito
                return Ok("Cliente creado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDto clienteDto)
        {
            try
            {
                await _generalDataInterface.UpdateClienteAsync(id, clienteDto);
                return Ok("Cliente actualizado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _generalDataInterface.DeleteClienteAsync(id);
                return Ok("Cliente eliminado con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update-contacto/by-id-cliente/{id}")]
        public async Task<IActionResult> UpdateContacto(int id, [FromBody] ContactoDto contactoDto)
        {
            try
            {
                await _generalDataInterface.UpdateContactoClienteAsync(id, contactoDto);
                return Ok("Información de contacto actualizada con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
