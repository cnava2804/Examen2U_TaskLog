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
    public class TaskListsController : Controller
    {
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext _context;
        private Pagination<TaskList> PaginationTaskList;

        public TaskListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskLists
        public async Task<IActionResult> Index(string search, int page = 1)
        {

            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }

            //Obtener los registros totales 
            totalRecords = await _context.TaskLists.CountAsync(
                    d => d.TaskListName.Contains(search));

            //Obtener la pagina de registros(datos)
            var taskLists = await _context.TaskLists
                .Where(d => d.TaskListName.Contains(search)).ToListAsync();

            var taskListsResult = taskLists.OrderBy(o => o.TaskListName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);

            //Obtener el total de paginas
            var totalPage = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //Instanciar la clase de paginacion
            PaginationTaskList = new Pagination<TaskList>()
            {
                RecordsPerPage = this.RecordsPerPage,
                TotalRecords = totalRecords,
                TotalPage = totalPage,
                CurrentPage = page,
                Search = search,
                Result = taskListsResult
            };

            return View(PaginationTaskList);
        }

        // GET: TaskLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .FirstOrDefaultAsync(m => m.TaskListId == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // GET: TaskLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskListId,TaskListName")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskList);
        }

        // GET: TaskLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists.FindAsync(id);
            if (taskList == null)
            {
                return NotFound();
            }
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskListId,TaskListName")] TaskList taskList)
        {
            if (id != taskList.TaskListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListExists(taskList.TaskListId))
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
            return View(taskList);
        }

        // GET: TaskLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .FirstOrDefaultAsync(m => m.TaskListId == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskList = await _context.TaskLists.FindAsync(id);
            _context.TaskLists.Remove(taskList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskListExists(int id)
        {
            return _context.TaskLists.Any(e => e.TaskListId == id);
        }
    }
}
