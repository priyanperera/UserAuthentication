using Application.Users.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register([FromForm] RegisterRequest registerRequest)
        {
            var response = await Mediator.Send(new RegisterCommand
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Username = registerRequest.Username,
                Password = registerRequest.Password,
            });

            return response.Status == RegisterStatus.Sucessful;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromForm] LoginRequest loginRequest)
        {
            var response = await Mediator.Send(new LoginCommand { Username = loginRequest.Username, Password = loginRequest.Password });

            if(response.Status != LoginStatus.Successful)
            {
                return Unauthorized();
            }
            return new LoginResponse 
            {
                FirstName = response.FirstName,
                LastName = response.LastName,
                Token = response.Token,
                UserId = response.UserId
            };
        }
    }
}
