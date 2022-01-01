using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenHandler
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}
