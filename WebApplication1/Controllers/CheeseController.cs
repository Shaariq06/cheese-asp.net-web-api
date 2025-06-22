using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheeseController : ControllerBase
    {
        private readonly ICheeseService _cheeseService;
        public CheeseController(ICheeseService cheeseService)
        {
            _cheeseService = cheeseService;
        }

        // GET: api/cheese
        [HttpGet]
        public async Task<IActionResult> GetAllCheeses()
        {
            var cheeses = await _cheeseService.GetAllCheesesAsync();
            return Ok(cheeses);
        }

        // GET: api/cheese/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheeseById(Guid id)
        {
            var cheese = await _cheeseService.GetCheeseByIdAsync(id);
            if (cheese != null)
                return Ok(cheese);
            return NotFound();
        }

        // POST: api/cheese
        [HttpPost]
        public async Task<IActionResult> CreateCheese([FromBody] Cheese cheese)
        {
            if (cheese == null)
                return BadRequest("Cheese data is required.");

            try
            {
                var created = await _cheeseService.CreateCheeseAsync(cheese);
                if (!created)
                    return BadRequest("Cheese could not be created.");
                return CreatedAtAction(nameof(GetCheeseById), new { id = cheese.Id }, cheese);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating cheese: {ex.Message}");
            }
        }

        // PUT: api/cheese/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheese(Guid id, [FromBody] Cheese cheese)
        {
            if (cheese == null)
                return BadRequest("Cheese data is required.");

            cheese.Id = id;

            try
            {
                var updated = await _cheeseService.UpdateCheeseAsync(id, cheese);
                if (!updated)
                    return NotFound($"Cheese with id {id} not found.");
                return Ok(new { Message = "Cheese updated successfully", CheeseId = id });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating cheese: {ex.Message}");
            }
        }

        // DELETE: api/cheese/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheese(Guid id)
        {
            var existingCheese = await _cheeseService.GetCheeseByIdAsync(id);
            if (existingCheese == null)
                return NotFound($"Cheese with id {id} not found.");

            try
            {
                var deleted = await _cheeseService.DeleteCheeseAsync(id);
                if (!deleted)
                    return BadRequest("Cheese could not be deleted.");
                return Ok(new { Message = "Cheese deleted successfully", CheeseId = id });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting cheese: {ex.Message}");
            }
        }
    }
}