using backendApi.Models;

namespace backendApi.Services
{
    public interface IUserService
    {
        Task<User> RegisterUser(User user);
        Task<User?> GetUserById(int id);
    }
}
