using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen2UTaskLog.Data;
using Examen2UTaskLog.Models;
using Examen2UTaskLog.Common;

namespace Examen2UTaskLog.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext _context;
        private Pagination<Assignment> PaginationAssignment;

        public AssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Assignments
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }
            //Obtener los registros totales 
            totalRecords = await _context.Assignments.Include(a => a.State).Include(a => a.Tag).Include(a => a.TaskList).CountAsync(
                    d => d.AssignmentkName.Contains(search));

            //Obtener la pagina de registros(datos)
            var states = await _context.Assignments.Include(a => a.State).Include(a => a.Tag).Include(a => a.TaskList)
                .Where(d => d.AssignmentkName.Contains(search)).ToListAsync();

            var statesResult = states.OrderBy(o => o.AssignmentkName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);

            //Obtener el total de paginas
            var totalPage = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //Instanciar la clase de paginacion
            PaginationAssignment = new Pagination<Assignment>()
            {
                RecordsPerPage = this.RecordsPerPage,
                TotalRecords = totalRecords,
                TotalPage = totalPage,
                CurrentPage = page,
                Search = search,
                Result = statesResult
            };
            return View(PaginationAssignment);
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.State)
                .Include(a => a.Tag)
                .Include(a => a.TaskList)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName");
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName");
            ViewData["TaskListId"] = new SelectList(_context.TaskLists, "TaskListId", "TaskListName");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssignmentId,AssignmentkName,AssignmentDescription,AssignmentInitialDate,AssignmentEndDate,StateId,TagId,TaskListId")] Assignment assignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", assignment.StateId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", assignment.TagId);
            ViewData["TaskListId"] = new SelectList(_context.TaskLists, "TaskListId", "TaskListName", assignment.TaskList);
            return View(assignment);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", assignment.StateId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", assignment.TagId);
            ViewData["TaskListId"] = new SelectList(_context.TaskLists, "TaskListId", "TaskListName", assignment.TaskList);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssignmentId,AssignmentkName,AssignmentDescription,AssignmentInitialDate,AssignmentEndDate,StateId,TagId,TaskListId")] Assignment assignment)
        {
            if (id != assignment.AssignmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.AssignmentId))
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
            ViewData["StateId"] = new SelectList(_context.States, "StateId", "StateName", assignment.StateId);
            ViewData["TagId"] = new SelectList(_context.Tags, "TagId", "TagName", assignment.TagId);
            ViewData["TaskListId"] = new SelectList(_context.TaskLists, "TaskListId", "TaskListName", assignment.TaskList);
            return View(assignment);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.State)
                .Include(a => a.Tag)
                .Include(a => a.TaskList)
                .FirstOrDefaultAsync(m => m.AssignmentId == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.AssignmentId == id);
        }
    }
}
