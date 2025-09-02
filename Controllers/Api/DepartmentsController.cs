using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Data;
using ClassroomAllocationApp.Models;

namespace ClassroomAllocationApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            return await _context.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);

            if (dept == null)
            {
                return NotFound();
            }

            return dept;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department dept)
        {
            _context.Departments.Add(dept);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = dept.Id }, dept);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department dept)
        {
            if (id != dept.Id)
            {
                return BadRequest();
            }

            _context.Entry(dept).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            if (dept == null)
            {
                return NotFound();
            }

            _context.Departments.Remove(dept);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
