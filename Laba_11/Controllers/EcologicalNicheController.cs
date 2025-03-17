using Laba_11.Data;
using Laba_11.Dtos;
using Laba_11.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laba_11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcologicalNicheController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EcologicalNicheController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EcologicalNiche
        [HttpGet]
        public async Task<IActionResult> GetNiches()
        {
            var niches = await _context.EcologicalNiches
                .Include(n => n.BirdEcologicalNiches)
                .ThenInclude(bn => bn.Bird)
                .ToListAsync();

            return Ok(niches.Select(n => n.ToDto()));
        }

        // GET: api/EcologicalNiche/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNiche([FromRoute] int id)
        {
            var niche = await _context.EcologicalNiches
                .Include(n => n.BirdEcologicalNiches)
                .ThenInclude(bn => bn.Bird)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (niche is null)
                return NotFound();

            return Ok(niche.ToDto());
        }

        // POST: api/EcologicalNiche
        [HttpPost]
        public async Task<IActionResult> CreateNiche([FromBody] CreateEcologicalNicheDto dto)
        {
            var niche = dto.ToModel();

            await _context.EcologicalNiches.AddAsync(niche);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNiche), new { id = niche.Id }, niche.ToDto());
        }

        // PUT: api/EcologicalNiche/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNiche([FromRoute] int id, UpdateEcologicalNicheDto dto)
        {
            var niche = await _context.EcologicalNiches.FirstOrDefaultAsync(n => n.Id == id);

            if (niche is null)
                return NotFound();

            dto.FromUpdateToModel(niche);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/EcologicalNiche/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNiche([FromRoute] int id)
        {
            var niche = await _context.EcologicalNiches
                .Include(n => n.BirdEcologicalNiches)
                .FirstOrDefaultAsync(n => n.Id == id);

            if (niche is null)
                return NotFound();

            _context.BirdEcologicalNiches.RemoveRange(niche.BirdEcologicalNiches);
            _context.EcologicalNiches.Remove(niche);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
