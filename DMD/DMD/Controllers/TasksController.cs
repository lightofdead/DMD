#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DMD.Data;
using DMD.Models;

namespace DMD.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tasks.Include(t => t.ParentTask);
            return View(await appDbContext.ToListAsync());
        }
        public IActionResult IndexAjax()
        {
            var tasks = _context.Tasks.Include(t => t.ParentTask);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.ParentTask)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ParentID")] Models.Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID", task.ParentID);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID", task.ParentID);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(/*int id, [Bind("ID,Name,ParentID")]*/ Models.Task task)
        {
            /*if (id != task.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }*/
            _context.Attach(task);
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            /*ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID", task.ParentID);
            return View(task);*/
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.ParentTask)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #region "Ajax Functions"
        [HttpPost]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult ViewTask(int? Id)
        {
            var task = _context.Tasks.Find(Id);

            ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID", task.ParentID);

            return PartialView("_detail", task);
        }

        public IActionResult EditTask(int Id)
        {
            Models.Task task = _context.Tasks.Find(Id);

            ViewData["ParentID"] = new SelectList(_context.Tasks, "ID", "ID", task.ParentID);
            return PartialView("_Edit", task);
        }

        [HttpPost]
        public IActionResult UpdateTask(Models.Task task)
        {
            _context.Attach(task);
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
            return PartialView("_Task", task);
        }
        #endregion

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.ID == id);
        }
    }
}
