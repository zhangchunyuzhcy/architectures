using Microsoft.EntityFrameworkCore;
using MyProject.Application.Common.Interfaces;
using MyProject.Domain.Users;
using MyProject.Infrastructure.Common.Persistence;

namespace MyProject.Infrastructure.Users;

public class UsersRepository(MyProjectDbContext _dbContext) : IUsersRepository
{
    public async Task AddUserAsync(User user)
    {
        await _dbContext.AddAsync(user);
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
    }

    public Task UpdateAsync(User user)
    {
        _dbContext.Update(user);

        return Task.CompletedTask;
    }
}