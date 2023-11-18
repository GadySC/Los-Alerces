using LosAlerces_DBManagement.Entities;
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
                var categorias = await _generalDataInterface.GetContactosListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
