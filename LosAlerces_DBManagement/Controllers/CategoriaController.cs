using LosAlerces_DBManagement.Entities;
using LosAlerces_DBManagement.Models.Dto;
using LosAlerces_DBManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LosAlerces_DBManagement.Controllers
{
    [Route("v1/categoria")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IGeneralDataInterface _generalDataInterface;

        public CategoriaController(IGeneralDataInterface generalDataInterface)
        {
            _generalDataInterface = generalDataInterface ?? throw new ArgumentNullException(nameof(generalDataInterface));
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            try
            {
                var categorias = await _generalDataInterface.GetCategoriasListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            try
            {
                var categoria = await _generalDataInterface.GetCategoriaByIdAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Categoria>> Create([FromBody] CategoriaDto model)
        {
            if (model == null)
            {
                return BadRequest("Datos de categoría inválidos.");
            }

            try
            {
                var categoriaDto = new Categoria
                {
                    Nombre_Categoria = model.Categoria
                };

                await _generalDataInterface.AddCategoriaAsync(categoriaDto);
                return CreatedAtAction(nameof(GetById), new { id = categoriaDto.ID_Categoria }, categoriaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoriaDto model)
        {
            if (model == null)
            {
                return BadRequest("Datos de categoría inválidos.");
            }

            try
            {
                var categoriaExistente = await _generalDataInterface.GetCategoriaByIdAsync(id);
                if (categoriaExistente == null)
                {
                    return NotFound($"Categoría con ID {id} no encontrada.");
                }

                categoriaExistente.Nombre_Categoria = model.Categoria;

                await _generalDataInterface.UpdateCategoriaAsync(categoriaExistente);
                return NoContent();
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
                var categoriaToDelete = await _generalDataInterface.GetCategoriaByIdAsync(id);
                if (categoriaToDelete == null)
                {
                    return NotFound();
                }

                await _generalDataInterface.DeleteCategoriaAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
