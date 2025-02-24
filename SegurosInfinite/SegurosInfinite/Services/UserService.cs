using SegurosInfinite.Data;
using SegurosInfinite.Models;
using SegurosInfinite.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SegurosInfinite.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUser()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return [];  // Return empty list
            }
        }

        public Task<User> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
