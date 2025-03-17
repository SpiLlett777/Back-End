using Laba_11.Data;
using Laba_11.Dtos;
using Laba_11.Mappers;
using Laba_11.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laba_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BirdController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/birds
        [HttpGet]
        public async Task<IActionResult> GetBirds()
        {
            var birds = await _context.Birds
                .Include(b => b.BirdEcologicalNiches)
                .ThenInclude(bn => bn.EcologicalNiche)
                .ToListAsync();

            return Ok(birds.Select(b => b.ToDto()));
        }

        // GET: api/birds/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBird([FromRoute] int id)
        {
            var bird = await _context.Birds
                .Include(b => b.BirdEcologicalNiches)
                .ThenInclude(bn => bn.EcologicalNiche)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bird is null)
                return NotFound();

            return Ok(bird.ToDto());
        }

        // POST: api/birds
        [HttpPost]
        public async Task<IActionResult> CreateBird([FromBody] CreateBirdDto dto)
        {
            var bird = dto.ToModel();

            if (dto.BirdEcologicalNicheIds.Any())
            {
                var niches = await _context.EcologicalNiches
                    .Where(n => dto.BirdEcologicalNicheIds.Contains(n.Id))
                    .ToListAsync();

                bird.BirdEcologicalNiches = niches
                    .Select(n => new BirdEcologicalNiche { Bird = bird, EcologicalNiche = n })
                    .ToList();
            }

            await _context.Birds.AddAsync(bird);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBird), new { id = bird.Id }, bird.ToDto());
        }

        // PUT: api/birds/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBird([FromRoute] int id, UpdateBirdDto dto)
        {
            var bird = await _context.Birds
                .Include(b => b.BirdEcologicalNiches)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bird is null)
                return NotFound();

            dto.FromUpdateToModel(bird);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/birds/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBird([FromRoute] int id)
        {
            var bird = await _context.Birds
                .Include(b => b.BirdEcologicalNiches)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bird is null)
                return NotFound();

            _context.Birds.Remove(bird);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
