using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Data;
using ClassroomAllocationApp.Models;

namespace ClassroomAllocationApp.Controllers
{
    public class AllocationsController : Controller
    {
        private readonly AppDbContext _context;

        public AllocationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Allocations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Allocations.Include(a => a.Classroom).Include(a => a.Course);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Allocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _context.Allocations
                .Include(a => a.Classroom)
                .Include(a => a.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        // GET: Allocations/Create
        public IActionResult Create()
        {
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id");
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: Allocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,ClassroomId,StartTime,DayOfWeek")] Allocation allocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(allocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", allocation.ClassroomId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", allocation.CourseId);
            return View(allocation);
        }

        // GET: Allocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _context.Allocations.FindAsync(id);
            if (allocation == null)
            {
                return NotFound();
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", allocation.ClassroomId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", allocation.CourseId);
            return View(allocation);
        }

        // POST: Allocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,ClassroomId,StartTime,DayOfWeek")] Allocation allocation)
        {
            if (id != allocation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(allocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AllocationExists(allocation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", allocation.ClassroomId);
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", allocation.CourseId);
            return View(allocation);
        }

        // GET: Allocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _context.Allocations
                .Include(a => a.Classroom)
                .Include(a => a.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (allocation == null)
            {
                return NotFound();
            }

            return View(allocation);
        }

        // POST: Allocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var allocation = await _context.Allocations.FindAsync(id);
            if (allocation != null)
            {
                _context.Allocations.Remove(allocation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AllocationExists(int id)
        {
            return _context.Allocations.Any(e => e.Id == id);
        }
    }
}
