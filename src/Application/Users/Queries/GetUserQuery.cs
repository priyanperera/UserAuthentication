using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class GetUserQuery : IRequest<GetUserQueryResponse>
    {
        public string Username { get; set; }
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private readonly IDataRepository dataRepository;

        public GetUserQueryHandler(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await dataRepository.GetFirst<User>(x => x.Username == request.Username);
            return new GetUserQueryResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }

    public class GetUserQueryResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
