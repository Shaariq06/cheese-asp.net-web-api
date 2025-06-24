using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;

namespace WebApplication1.Repositories
{
    public class CheeseRepository : ICheeseRepository
    {
        private readonly CheeseDbContext _context;

        public CheeseRepository(CheeseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cheese>> GetAllCheesesAsync()
        {
            return await _context.Cheeses.ToListAsync();
        }

        public async Task<Cheese?> GetCheeseByIdAsync(Guid id)
        {
            return await _context.Cheeses.FindAsync(id);
        }

        public async Task<Cheese?> CreateCheeseAsync(Cheese cheese)
        {
            _context.Cheeses.Add(cheese);
            await _context.SaveChangesAsync();
            return cheese;
        }

        public async Task<Cheese?> UpdateCheeseAsync(Guid id, Cheese cheese)
        {
            var existingCheese = await _context.Cheeses.FindAsync(id);
            if (existingCheese == null) return null;

            existingCheese.Name = cheese.Name;
            existingCheese.Type = cheese.Type;
            existingCheese.Country = cheese.Country;
            existingCheese.PricePerKilo = cheese.PricePerKilo;
            existingCheese.StinkinessRating = cheese.StinkinessRating;
            existingCheese.Aged = cheese.Aged;
            existingCheese.PairingSuggestion = cheese.PairingSuggestion;

            await _context.SaveChangesAsync();
            return existingCheese;
        }

        public async Task<bool> DeleteCheeseAsync(Guid id)
        {
            var cheese = await _context.Cheeses.FindAsync(id);
            if (cheese == null)
                return false;

            _context.Cheeses.Remove(cheese);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}