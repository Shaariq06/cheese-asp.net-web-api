using WebApplication1.Models.Domain;

namespace WebApplication1.Services
{
    public interface ICheeseService
    {
        Task<IEnumerable<Cheese>> GetAllCheesesAsync();
        Task<Cheese?> GetCheeseByIdAsync(Guid id);
        Task<bool> CreateCheeseAsync(Cheese cheese);
        Task<bool> UpdateCheeseAsync(Guid id, Cheese cheese);
        Task<bool> DeleteCheeseAsync(Guid id);
    }
}