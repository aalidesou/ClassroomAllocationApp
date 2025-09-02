using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Data;
using ClassroomAllocationApp.Models;

namespace ClassroomAllocationApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllocationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AllocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/allocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Allocation>>> GetAllocations()
        {
            return await _context.Allocations
                .Include(a => a.Course)
                .Include(a => a.Classroom)
                .ToListAsync();
        }

        // POST: api/allocations
        [HttpPost]
        public async Task<ActionResult<Allocation>> CreateAllocation(Allocation allocation)
        {
            // ✅ Prevent overlapping allocations in the same classroom
            bool conflict = await _context.Allocations.AnyAsync(a =>
                a.ClassroomId == allocation.ClassroomId &&
                (allocation.StartTime >= a.StartTime && allocation.StartTime < a.EndTime ||
                 allocation.EndTime > a.StartTime && allocation.EndTime <= a.EndTime));

            if (conflict)
            {
                return BadRequest("❌ This classroom is already allocated for that time slot.");
            }

            _context.Allocations.Add(allocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllocations), new { id = allocation.Id }, allocation);
        }

        // DELETE: api/allocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAllocation(int id)
        {
            var allocation = await _context.Allocations.FindAsync(id);
            if (allocation == null)
            {
                return NotFound();
            }

            _context.Allocations.Remove(allocation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
