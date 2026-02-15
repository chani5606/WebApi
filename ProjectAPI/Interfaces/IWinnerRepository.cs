using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface IWinnerRepository
    {
        Task<Winner> Create(Winner w);
        Task<List<Winner>> GetAll();
        Task<Winner?> GetById(int id);
    }
}