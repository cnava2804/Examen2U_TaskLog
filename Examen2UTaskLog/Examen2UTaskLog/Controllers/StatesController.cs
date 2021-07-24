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
    public class StatesController : Controller
    {
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext _context;
        private Pagination<State> PaginationStates;

        public StatesController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: States
        public async Task<IActionResult> Index(string search, int page = 1)
        {
            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }

            //Obtener los registros totales 
            totalRecords = await _context.States.CountAsync(
                    d => d.StateName.Contains(search));

            //Obtener la pagina de registros(datos)
            var states = await _context.States
                .Where(d => d.StateName.Contains(search)).ToListAsync();

            var statesResult = states.OrderBy(o => o.StateName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);

            //Obtener el total de paginas
            var totalPage = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //Instanciar la clase de paginacion
            PaginationStates = new Pagination<State>()
            {
                RecordsPerPage = this.RecordsPerPage,
                TotalRecords = totalRecords,
                TotalPage = totalPage,
                CurrentPage = page,
                Search = search,
                Result = statesResult
            };

            return View(PaginationStates);
        }

        // GET: States/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // GET: States/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateName")] State state)
        {
            if (ModelState.IsValid)
            {
                _context.Add(state);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(state);
        }

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            return View(state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateName")] State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            return View(state);
        }

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.States
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _context.States.FindAsync(id);
            _context.States.Remove(state);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StateExists(int id)
        {
            return _context.States.Any(e => e.StateId == id);
        }
    }
}
