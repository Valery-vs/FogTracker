namespace FogTracker.Web.Controllers
{
    using Contracts.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ViewModel;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthService authService;

        public AccountController(IAuthService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponse> LoginByPassword([FromBody] LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var token = this.authService.Authenticate(model.Username, model.Password);
            return new OkObjectResult(new LoginResponse { Token = token });
        }
    }
}