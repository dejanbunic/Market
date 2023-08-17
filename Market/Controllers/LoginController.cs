using MapsterMapper;
using Market.Application.Models;
using Market.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Market.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        public LoginController(ILoginService loginService, IMapper mapper)
        {
            this._loginService = loginService;
            this._mapper = mapper;
        }

        [HttpPost("authenicate")]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var token = await _loginService.LoginAsync(credentials);
            return Ok(token);
        }
    }
}
