using MyProject.Domain.Users;

namespace MyProject.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}