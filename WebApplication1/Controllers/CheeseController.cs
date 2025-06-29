using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CheeseController : ControllerBase
    {
        private readonly ICheeseService _cheeseService;
        private readonly IMapper _mapper;
        public CheeseController(ICheeseService cheeseService, IMapper mapper)
        {
            _cheeseService = cheeseService;
            _mapper = mapper;
        }

        // GET: api/cheese
        [HttpGet]
        public async Task<IActionResult> GetAllCheeses()
        {
            var cheesesDomain = await _cheeseService.GetAllCheesesAsync();
            return Ok(_mapper.Map<IEnumerable<CheeseDTO>>(cheesesDomain));
        }

        // GET: api/cheese/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCheeseById(Guid id)
        {
            var cheeseDomain = await _cheeseService.GetCheeseByIdAsync(id);
            if (cheeseDomain != null)
                return Ok(_mapper.Map<CheeseDTO>(cheeseDomain));
            return NotFound();
        }

        // POST: api/cheese
        [HttpPost]
        public async Task<IActionResult> CreateCheese([FromBody] CheeseDTO cheeseDto)
        {
            try
            {
                var created = await _cheeseService.CreateCheeseAsync(_mapper.Map<Cheese>(cheeseDto));
                if (!created)
                    return BadRequest("Cheese could not be created.");
                return CreatedAtAction("Cheese Created", cheeseDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating cheese: {ex.Message}");
            }
        }

        // PUT: api/cheese/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheese([FromRoute] Guid id, [FromBody] CheeseDTO cheeseDto)
        {
            try
            {
                var updated = await _cheeseService.UpdateCheeseAsync(id, _mapper.Map<Cheese>(cheeseDto));
                if (!updated)
                    return NotFound($"Cheese with id {id} not found.");
                return Ok(new { Message = "Cheese updated successfully", cheeseDto });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating cheese: {ex.Message}");
            }
        }

        // DELETE: api/cheese/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheese([FromBody] Guid id)
        {
            var existingCheese = await _cheeseService.GetCheeseByIdAsync(id);
            if (existingCheese == null)
                return NotFound($"Cheese with id {id} not found.");

            try
            {
                var deleted = await _cheeseService.DeleteCheeseAsync(id);
                if (!deleted)
                    return BadRequest("Cheese could not be deleted.");
                return Ok(new { Message = "Cheese deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting cheese: {ex.Message}");
            }
        }
    }
}