using Application.Users.Queries;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public class GetUsersResponse
    {
        public IList<UserDto> Users { get; set; }
    }
}
