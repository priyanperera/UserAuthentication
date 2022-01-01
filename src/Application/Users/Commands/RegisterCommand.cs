using Application.Common.Interfaces;
using Application.Security;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands
{
    public class RegisterCommand : IRequest<RegisterCommandResponse>
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResponse>
    {
        private readonly IDataRepository dataRepository;

        public RegisterCommandHandler(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        public async Task<RegisterCommandResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = await dataRepository.GetFirst<User>(x => x.Username == request.Username);
            if (user != null)
            {
                return new RegisterCommandResponse { Status = RegisterStatus.UserExists };
            }

            var hashed = EncryptionHelper.HashPassword(request.Password);
            var entity = new User
            {
                Username = request.Username,
                Password = hashed,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            await dataRepository.Create(entity);

            return new RegisterCommandResponse
            {
                Status = RegisterStatus.Sucessful,
                UserId = entity.UserId
            };
        }
    }

    public class RegisterCommandResponse
    {
        public int? UserId { get; set; }
        public RegisterStatus Status { get; set; }
    }

    public enum RegisterStatus
    {
        Sucessful,
        UserExists
    }
}
