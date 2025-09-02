using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Data;

namespace ClassroomAllocationApp.Controllers
{
    public class VenueChangeRequestsController : Controller
    {
        private readonly AppDbContext _context;

        public VenueChangeRequestsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: VenueChangeRequests
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.VenueChangeRequests.Include(v => v.Allocation).Include(v => v.NewClassroom).Include(v => v.Teacher);
            return View(await appDbContext.ToListAsync());
        }

        // GET: VenueChangeRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueChangeRequest = await _context.VenueChangeRequests
                .Include(v => v.Allocation)
                .Include(v => v.NewClassroom)
                .Include(v => v.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venueChangeRequest == null)
            {
                return NotFound();
            }

            return View(venueChangeRequest);
        }

        // GET: VenueChangeRequests/Create
        public IActionResult Create()
        {
            ViewData["AllocationId"] = new SelectList(_context.Allocations, "Id", "Id");
            ViewData["NewClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }

        // POST: VenueChangeRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,AllocationId,NewClassroomId,RequestedDate,Status")] VenueChangeRequest venueChangeRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venueChangeRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AllocationId"] = new SelectList(_context.Allocations, "Id", "Id", venueChangeRequest.AllocationId);
            ViewData["NewClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", venueChangeRequest.NewClassroomId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", venueChangeRequest.TeacherId);
            return View(venueChangeRequest);
        }

        // GET: VenueChangeRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueChangeRequest = await _context.VenueChangeRequests.FindAsync(id);
            if (venueChangeRequest == null)
            {
                return NotFound();
            }
            ViewData["AllocationId"] = new SelectList(_context.Allocations, "Id", "Id", venueChangeRequest.AllocationId);
            ViewData["NewClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", venueChangeRequest.NewClassroomId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", venueChangeRequest.TeacherId);
            return View(venueChangeRequest);
        }

        // POST: VenueChangeRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,AllocationId,NewClassroomId,RequestedDate,Status")] VenueChangeRequest venueChangeRequest)
        {
            if (id != venueChangeRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venueChangeRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueChangeRequestExists(venueChangeRequest.Id))
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
            ViewData["AllocationId"] = new SelectList(_context.Allocations, "Id", "Id", venueChangeRequest.AllocationId);
            ViewData["NewClassroomId"] = new SelectList(_context.Classrooms, "Id", "Id", venueChangeRequest.NewClassroomId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", venueChangeRequest.TeacherId);
            return View(venueChangeRequest);
        }

        // GET: VenueChangeRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venueChangeRequest = await _context.VenueChangeRequests
                .Include(v => v.Allocation)
                .Include(v => v.NewClassroom)
                .Include(v => v.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venueChangeRequest == null)
            {
                return NotFound();
            }

            return View(venueChangeRequest);
        }

        // POST: VenueChangeRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueChangeRequest = await _context.VenueChangeRequests.FindAsync(id);
            if (venueChangeRequest != null)
            {
                _context.VenueChangeRequests.Remove(venueChangeRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VenueChangeRequestExists(int id)
        {
            return _context.VenueChangeRequests.Any(e => e.Id == id);
        }
    }
}
