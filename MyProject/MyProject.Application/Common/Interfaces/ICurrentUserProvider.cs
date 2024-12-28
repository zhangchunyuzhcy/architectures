using MyProject.Application.Common.Models;

namespace MyProject.Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}