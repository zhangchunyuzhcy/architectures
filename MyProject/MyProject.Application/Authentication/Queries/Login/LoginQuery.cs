using ErrorOr;

using MediatR;
using MyProject.Application.Authentication.Common;

namespace MyProject.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;