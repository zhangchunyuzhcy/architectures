using MyProject.Domain.Users;

namespace MyProject.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);