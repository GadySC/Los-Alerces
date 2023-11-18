using LosAlerces_DBManagement.Entities;
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
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAllClientes()
        {
            try
            {
                var categorias = await _generalDataInterface.GetClienteListAsync();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
