using ProjectAPI.Models;

namespace ProjectAPI.Interfaces
{
    public interface ICatgoryRepository
    {
        Task<Catgories> CreateCatgory(Catgories category);
        Task<bool> DeleteCatgory(int id);
        Task<bool> ExistsCatgory(int id);
        Task<List<Catgories>> GetAllCatgories();
        Task<Catgories?> GetByICatgory(int id);
        Task<Catgories?> UpdateCatgory(Catgories category);
    }
}