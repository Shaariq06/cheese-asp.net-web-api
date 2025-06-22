using WebApplication1.Models.Domain;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CheeseService : ICheeseService
    {
        private readonly ICheeseRepository _cheeseRepository;

        public CheeseService(ICheeseRepository cheeseRepository)
        {
            _cheeseRepository = cheeseRepository;
        }

        public async Task<IEnumerable<Cheese>> GetAllCheesesAsync()
        {
            return await _cheeseRepository.GetAllCheesesAsync();
        }

        public async Task<Cheese?> GetCheeseByIdAsync(Guid id)
        {
            return await _cheeseRepository.GetCheeseByIdAsync(id);
        }

        public async Task<bool> CreateCheeseAsync(Cheese cheese)
        {
            if (cheese == null)
                throw new ArgumentNullException(nameof(cheese), "Cheese cannot be null.");

            cheese.Id = Guid.NewGuid();

            if (string.IsNullOrWhiteSpace(cheese.Name))
                throw new ArgumentException("Cheese name is required.", nameof(cheese.Name));

            if (cheese.PricePerKilo <= 0)
                throw new ArgumentOutOfRangeException(nameof(cheese.PricePerKilo), "Price per kilo must be greater than zero.");

            if (cheese.StinkinessRating <= 0 || cheese.StinkinessRating > 10)
                throw new ArgumentOutOfRangeException(nameof(cheese.StinkinessRating), "Stinkiness rating must be between 1 and 10.");

            var allCheeses = await _cheeseRepository.GetAllCheesesAsync();
            if (allCheeses.Any(c => c.Name == cheese.Name))
                throw new InvalidOperationException("A cheese with this name already exists.");

            var createdCheese = await _cheeseRepository.CreateCheeseAsync(cheese);
            return createdCheese != null;
        }

        public async Task<bool> UpdateCheeseAsync(Guid id, Cheese cheese)
        {
            if (cheese == null)
                throw new ArgumentNullException(nameof(cheese), "Cheese cannot be null.");

            if (string.IsNullOrWhiteSpace(cheese.Name))
                throw new ArgumentException("Cheese name is required.", nameof(cheese.Name));

            if (cheese.PricePerKilo <= 0)
                throw new ArgumentOutOfRangeException(nameof(cheese.PricePerKilo), "Price per kilo must be greater than zero.");

            if (cheese.StinkinessRating <= 0 || cheese.StinkinessRating > 10)
                throw new ArgumentOutOfRangeException(nameof(cheese.StinkinessRating), "Stinkiness rating must be between 1 and 10.");

            var allCheeses = await _cheeseRepository.GetAllCheesesAsync();
            if (allCheeses.Any(c => c.Id != id && c.Name == cheese.Name))
                throw new InvalidOperationException("A cheese with this name already exists.");

            var updatedCheese = await _cheeseRepository.UpdateCheeseAsync(id, cheese);
            return updatedCheese != null;
        }

        public async Task<bool> DeleteCheeseAsync(Guid id)
        {
            return await _cheeseRepository.DeleteCheeseAsync(id);
        }
    }
}