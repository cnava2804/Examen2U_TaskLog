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
    public class TagsController : Controller
    {
        private readonly int RecordsPerPage = 10;
        private readonly ApplicationDbContext _context;
        private Pagination<Tag> PaginationTags;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: Tags
        public async Task<IActionResult> Index(string search, int page = 1)
        {

            int totalRecords = 0;

            if (search == null)
            {
                search = "";
            }

            //Obtener los registros totales 
            totalRecords = await _context.Tags.CountAsync(
                    d => d.TagName.Contains(search));

            //Obtener la pagina de registros(datos)
            var tags = await _context.Tags
                .Where(d => d.TagName.Contains(search)).ToListAsync();

            var tagsResult = tags.OrderBy(o => o.TagName)
                .Skip((page - 1) * RecordsPerPage)
                .Take(RecordsPerPage);

            //Obtener el total de paginas
            var totalPage = (int)Math.Ceiling((double)totalRecords / RecordsPerPage);

            //Instanciar la clase de paginacion
            PaginationTags = new Pagination<Tag>()
            {
                RecordsPerPage = this.RecordsPerPage,
                TotalRecords = totalRecords,
                TotalPage = totalPage,
                CurrentPage = page,
                Search = search,
                Result = tagsResult
            };

            return View(PaginationTags);
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,TagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,TagName")] Tag tag)
        {
            if (id != tag.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagId))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.TagId == id);
        }
    }
}
