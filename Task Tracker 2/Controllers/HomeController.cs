using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task_Tracker_2.Models;

namespace Task_Tracker_2.Controllers
{
    public class HomeController : Controller
    {

        private readonly ToDoContext context;

        public HomeController(ToDoContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var tasks = context.toDos.Include(t => t.Title).Include(t => t.Status).OrderBy(t => t.DueDate).ToList();
            return View(tasks);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Titles = context.titles.ToList();
            ViewBag.Statuses = context.statuses.ToList();
            var task = new ToDo { StatusId = "open" };
            return View(task);
        }
        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                context.toDos.Add(task);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.titles = context.titles.ToList();
                ViewBag.Statuses = context.statuses.ToList();
                return View(task);
            }
        }


        [HttpPost]
        public IActionResult MarkDone([FromRoute] string id, ToDo selected)
        {
            selected = context.toDos.Find(selected.Id)!;
            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }
            return RedirectToAction("Index", new { ID = id });
        }
        [HttpPost]
        public IActionResult DeleteDone(string id)
        {
            var toDelete = context.toDos.Where(t => t.StatusId == "closed").ToList();
            foreach (var task in toDelete)
            {
                context.toDos.Remove(task);
            }
            context.SaveChanges();
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = context.toDos.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            ViewBag.Titles = context.titles.ToList();
            ViewBag.Statuses = context.statuses.ToList();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(int id, ToDo task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.toDos.Update(task);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.Titles = context.titles.ToList();
            ViewBag.Statuses = context.statuses.ToList();
            return View(task);
        }

        private bool TaskExists(int id)
        {
            return context.toDos.Any(t => t.Id == id);
        }
       
    }
}




    

