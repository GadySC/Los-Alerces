using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/contactos")]
    [ApiController]
    public class ContactosController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public ContactosController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetAllContactos()
        {
            try
            {
                var contactos = await _generalDataInterface.GetAllContactosAsync();
                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Contactos>> CreateContacto([FromBody] ContactoDto contactoDto)
        {
            try
            {
                var newContacto = new Contactos
                {
                    name = contactoDto.name,
                    lastname = contactoDto.lastname,
                    email = contactoDto.email,
                    phone = contactoDto.phone,
                    ID_Cliente = contactoDto.ID_Cliente
                };

                await _generalDataInterface.AddContactoAsync(newContacto);
                return CreatedAtAction(nameof(GetContactoById), new { id = newContacto.ID_Contactos }, newContacto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateContacto(int id, [FromBody] ContactoDto contactoDto)
        {
            try
            {
                var contactoToUpdate = await _generalDataInterface.GetContactoByIdAsync(id);
                if (contactoToUpdate == null)
                {
                    return NotFound($"Contacto con ID {id} no encontrado.");
                }

                contactoToUpdate.name = contactoDto.name;
                contactoToUpdate.lastname = contactoDto.lastname;
                contactoToUpdate.email = contactoDto.email;
                contactoToUpdate.phone = contactoDto.phone;
                contactoToUpdate.ID_Cliente = contactoDto.ID_Cliente;

                await _generalDataInterface.UpdateContactoAsync(contactoToUpdate);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteContacto(int id)
        {
            try
            {
                await _generalDataInterface.DeleteContactoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filtered/by/{id}")]
        public async Task<ActionResult<Contactos>> GetContactoById(int id)
        {
            try
            {
                var contacto = await _generalDataInterface.GetContactoByIdAsync(id);
                if (contacto == null)
                {
                    return NotFound();
                }
                return Ok(contacto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
