using Microsoft.EntityFrameworkCore;
using MyStocks.Domain.Users;
using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public void CreateUser(User User)
    {
        _context.Users.Add(User);
    }

    public async void DeleteUser(Guid Id)
    {
        var user  = await GetUserByIdAsync(Id);
        if (user is not null)
            _context.Users.Remove(user);
    }

    public async Task<User?> GetUserByIdAsync(Guid Id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == Id);
    }

    public void UpdateUser(User User)
    {
        _context.Entry(User).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
    }

    public async Task<bool> EmailIsUniqueAsync(Email Email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == Email) is null;
    }
}
