using ProjectAPI.DTOs;

namespace ProjectAPI.Interfaces
{
    public interface IWinnerServises
    {
        Task<WinnerResponseDTO> Create(WinnerCreatedDTO w);
        Task<List<WinnerResponseDTO>> GetAll();
        Task<WinnerResponseDTO?> GetById(int id);
    }
}