using Application.Users.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class UserController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<GetUsersResponse>> Get(int pageNumber, int pageSize)
        {
            var results = await Mediator.Send(new GetUsersQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
            });

            return new GetUsersResponse
            {
                Users = results.Users
            };
        }
    }
}
