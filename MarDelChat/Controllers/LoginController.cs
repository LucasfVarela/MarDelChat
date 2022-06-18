using API_CoreBusiness.Authentication.Request;
using API_CoreBusiness.Authentication.Response;
using API_UsesCases.Services;
using API_UsesCases.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Consesinaria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserService usuarioService;

        public LoginController(IUnitOfWork unitOfWork, IUserService usuarioService )
        {
            this.unitOfWork = unitOfWork;
            this.usuarioService = usuarioService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] UserRequest req)
        {
            var response = usuarioService.Login(req.Email, req.Password);
            if (response == null)
            {
                return Unauthorized();
            }
             var token = usuarioService.GetToken(response);
            return Ok(new
            {
                token = token,
                usuario = response

            });
        }

        [HttpPost("Registro")]
        public ActionResult RegistrarUsuario([FromBody] UserRequest user)
        {
            if (unitOfWork.UsuarioRepo.ExisteUsuario(user.Email.ToLower()))
            {
                return BadRequest("Cuenta ya esxiste");
            }
            UserResponse res = usuarioService.Registrar(user, user.Password);
            return Ok(res);
        }
    }
}
