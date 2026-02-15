using ProjectAPI.Models;

namespace ProjectAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<List<User>> GetAllUsers();
        Task<User?> GetUsersByID(int id);
        Task<User?> UpdateGifts(User user);
        Task<bool> EmailExists(string email);
        Task<User?> GetByEmail(string email);


    }
}