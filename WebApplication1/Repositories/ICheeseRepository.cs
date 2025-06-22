using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public interface ICheeseRepository
    {
        Task<IEnumerable<Cheese>> GetAllCheesesAsync();
        Task<Cheese?> GetCheeseByIdAsync(Guid id);
        Task<Cheese?> CreateCheeseAsync(Cheese cheese);
        Task<Cheese?> UpdateCheeseAsync(Guid id, Cheese cheese);
        Task<bool> DeleteCheeseAsync(Guid id);
    }
}