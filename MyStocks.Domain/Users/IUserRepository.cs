using MyStocks.Domain.Users.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStocks.Domain.Users;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid Id);
    public void UpdateUser(User User);
    public void DeleteUser(Guid Id);
    public void CreateUser(User User);
    public Task<bool> EmailIsUniqueAsync(Email Email);
}
