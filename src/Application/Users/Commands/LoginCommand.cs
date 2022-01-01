using Application.Common.Interfaces;
using Application.Security;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly IDataRepository dataRepository;
        private readonly IJwtTokenHandler jwtTokenHandler;

        public LoginCommandHandler(
            IDataRepository dataRepository,
            IJwtTokenHandler jwtTokenHandler)
        {
            this.dataRepository = dataRepository;
            this.jwtTokenHandler = jwtTokenHandler;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await dataRepository.GetFirst<User>(x => x.Username == request.Username);
            if (user == null)
            {
                return new LoginCommandResponse { Status = LoginStatus.UserDoesNotExist };
            }

            var result = EncryptionHelper.Verify(request.Password, user.Password);

            if (!result)
            {
                return new LoginCommandResponse { Status = LoginStatus.InvalidPassword };
            }

            var token = jwtTokenHandler.GenerateToken(user);

            return new LoginCommandResponse
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token,
                Status = LoginStatus.Successful,
            };
        }
    }

    public class LoginCommandResponse
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public LoginStatus Status { get; set; }
    }

    public enum LoginStatus
    {
        Successful,
        UserDoesNotExist,
        InvalidPassword
    }
}
