using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Data;
using ClassroomAllocationApp.Models;

namespace ClassroomAllocationApp.Controllers.Api
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
            var requests = await _context.VenueChangeRequests
                .Include(v => v.Allocation)
                    .ThenInclude(a => a.Course)
                .Include(v => v.Allocation)
                    .ThenInclude(a => a.Classroom)
                .ToListAsync();

            return View(requests);
        }

        // GET: VenueChangeRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var request = await _context.VenueChangeRequests
                .Include(v => v.Allocation)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (request == null) return NotFound();

            return View(request);
        }

        // GET: VenueChangeRequests/Create
        public IActionResult Create()
        {
            ViewData["Allocations"] = _context.Allocations
                .Include(a => a.Course)
                .Include(a => a.Classroom)
                .ToList();

            return View();
        }

        // POST: VenueChangeRequests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AllocationId,Reason")] VenueChangeRequest request)
        {
            if (ModelState.IsValid)
            {
                request.Status = "Pending";   // default status
                request.RequestedDate = DateTime.Now;

                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // POST: VenueChangeRequests/Approve/5
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var request = await _context.VenueChangeRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = "Approved";
            _context.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: VenueChangeRequests/Reject/5
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var request = await _context.VenueChangeRequests.FindAsync(id);
            if (request == null) return NotFound();

            request.Status = "Rejected";
            _context.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
