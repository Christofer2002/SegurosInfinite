using SegurosInfinite.Models;

namespace SegurosInfinite.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        Task<List<User>> GetAllUser();
    }
}
