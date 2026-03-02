using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
