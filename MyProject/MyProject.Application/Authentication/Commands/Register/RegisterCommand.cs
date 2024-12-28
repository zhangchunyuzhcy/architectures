using ErrorOr;

using MediatR;
using MyProject.Application.Authentication.Common;

namespace MyProject.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;