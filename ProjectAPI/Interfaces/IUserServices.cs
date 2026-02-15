using ProjectAPI.DTOs;

namespace ProjectAPI.Interfaces
{
    public interface IUserServices
    {
        Task<UserResponseDTO?> CreateUser(UserCreateDTO d);
        Task<bool> DeleteUser(int id);
        Task<List<UserResponseDTO>> GetAllUsers();
        Task<UserResponseDTO?> GetUserById(int id);
        Task<UserResponseDTO> UpdateUser(UserUpdateDTO d, int id);
        Task<AuthResponseDTO?> Authenticate(string email,string password);

    }
}