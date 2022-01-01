using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUsersQuery : IRequest<GetUsersQueryResponse>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersQueryResponse>
    {
        private readonly IDataRepository dataRepository;

        public GetUsersQueryHandler(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<GetUsersQueryResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var results = dataRepository.Query<User>();
            results = results.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);

            return new GetUsersQueryResponse
            {
                Users = results.Select(x => new UserDto
                {
                    UserId = x.UserId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username,
                }).ToList(),
            };
        }
    }

    public class GetUsersQueryResponse
    {
        public IList<UserDto> Users { get; set; }
    }

    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
    }
}
