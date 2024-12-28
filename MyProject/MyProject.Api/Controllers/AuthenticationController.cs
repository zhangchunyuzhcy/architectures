using ErrorOr;
using MyProject.Application.Authentication.Commands.Register;
using MyProject.Application.Authentication.Common;
using MyProject.Application.Authentication.Queries.Login;
using MyProject.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class AuthenticationController(ISender _mediator) : ApiController
{
    [HttpPost("login")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)] // Successful response
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)] // Validation error
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)] // Not found
    [ProducesResponseType(typeof(object), StatusCodes.Status403Forbidden)] // Unauthorized
    [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)] // Conflict
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var authResult = await _mediator.Send(query);

        return
            authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials
            ? Problem(
                detail: authResult.FirstError.Description,
                statusCode: StatusCodes.Status401Unauthorized
            )
            : authResult.Match(authResult => Ok(MapToAuthResponse(authResult)), Problem);
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)] // Successful response
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)] // Validation error
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)] // Not found
    [ProducesResponseType(typeof(object), StatusCodes.Status403Forbidden)] // Unauthorized
    [ProducesResponseType(typeof(object), StatusCodes.Status409Conflict)] // Conflict
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)] // Internal server error
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );

        var authResult = await _mediator.Send(command);

        return authResult.Match(authResult => base.Ok(MapToAuthResponse(authResult)), Problem);
    }

    private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
    }
}