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
        public async Task<ActionResult> GetAllClientes()
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

        [HttpPost("create")]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteDto clienteDto)
        {
            if (clienteDto == null)
            {
                return BadRequest("Datos del cliente son inválidos.");
            }

            var cliente = new Cliente
            {
                name = clienteDto.name,
                address = clienteDto.address,
                phone = clienteDto.phone,
                email = clienteDto.email
            };

            try
            {
                await _generalDataInterface.CreateClienteAsync(cliente);
                return CreatedAtAction(nameof(GetClienteById), new { id = cliente.ID_Cliente }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDto clienteDto)
        {
            try
            {
                var clienteToUpdate = await _generalDataInterface.GetClienteByIdAsync(id);
                if (clienteToUpdate == null)
                {
                    return NotFound($"Cliente con ID {id} no encontrado.");
                }

                clienteToUpdate.name = clienteDto.name;
                clienteToUpdate.address = clienteDto.address;
                clienteToUpdate.phone = clienteDto.phone;
                clienteToUpdate.email = clienteDto.email;

                await _generalDataInterface.UpdateClienteAsync(clienteToUpdate);
                return Ok(clienteToUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                var clienteToDelete = await _generalDataInterface.GetClienteByIdAsync(id);
                if (clienteToDelete == null)
                {
                    return NotFound();
                }

                await _generalDataInterface.DeleteClienteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filtered/by/{id}")]
        public async Task<IActionResult> GetClienteById(int id)
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
    }
}
