using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/personal")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public PersonalController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Personal>>> GetAllPersonal()
        {
            try
            {
                var categorias = await _generalDataInterface.GetPersonalListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
 

        [HttpPost("create")]
        public async Task<ActionResult<Personal>> Create([FromBody] PersonalDto personalDto)
        {
            if (personalDto == null)
            {
                return BadRequest("Datos de personal inválidos.");
            }

            var personal = new Personal
            {
                name = personalDto.name,
                lastname = personalDto.lastname,
                profession = personalDto.profession,
                salary = personalDto.salary,
                email = personalDto.email,
                address = personalDto.address,
                phone = personalDto.phone
            };

            await _generalDataInterface.AddPersonalAsync(personal);
            return CreatedAtAction(nameof(GetPersonalById), new { id = personal.ID_Personal }, personal);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] PersonalDto personalDto)
        {
            var existingPersonal = await _generalDataInterface.GetPersonalByIdAsync(id);
            if (existingPersonal == null)
            {
                return NotFound($"Personal con ID {id} no encontrado.");
            }

            existingPersonal.name = personalDto.name;
            existingPersonal.lastname = personalDto.lastname;
            existingPersonal.profession = personalDto.profession;
            existingPersonal.salary = personalDto.salary;
            existingPersonal.email = personalDto.email;
            existingPersonal.address = personalDto.address;
            existingPersonal.phone = personalDto.phone;

            try
            {
                await _generalDataInterface.UpdatePersonalAsync(existingPersonal);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personal = await _generalDataInterface.GetPersonalByIdAsync(id);
            if (personal == null)
            {
                return NotFound();
            }

            await _generalDataInterface.DeletePersonalAsync(id);
            return NoContent();
        }

        [HttpGet("filtered/by/{id}")]
        public async Task<ActionResult<Personal>> GetPersonalById(int id)
        {
            var personal = await _generalDataInterface.GetPersonalByIdAsync(id);
            if (personal == null)
            {
                return NotFound();
            }
            return Ok(personal);
        }
    }
}
