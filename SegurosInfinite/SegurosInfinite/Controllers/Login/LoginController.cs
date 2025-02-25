using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SegurosInfinite.Models;

namespace SegurosInfinite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        // GET: LoginController
        [HttpPost]
        [Consumes("application/json")]
        public IActionResult registerClient([FromBody] Client client)
        {
            try
            {
                Service.Instance().registerClient(client);
                return Ok();
            }
            catch (Exception e)
            {
                // Handle any exceptions or errors that may occur during registration

                return StatusCode(500, "Error al registrar el cliente");
            }
        }
    }

}

