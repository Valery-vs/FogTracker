namespace FogTracker.Web.Controllers
{
    using System.Linq;
    using Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Model.Entities;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IFogRepository fogRepository;

        public AccountController(IFogRepository fogRepository)
        {
            this.fogRepository = fogRepository;
        }


        [HttpPost("login")]
        public IActionResult LoginByPassword(string login, string password)
        {
            return this.Ok();
        }
    }
}