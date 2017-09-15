using BugTracker.Models;
using BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class BugsController : Controller
    {
        public ApplicationDbContext _context { get; set; }
        public BugsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Bugs
        public ActionResult Index()
        {
            IEnumerable<Bug> bugs = _context.Bugs.Include(b => b.Priority).ToList();
            return View(bugs);
        }

        public ActionResult New()
        {
            BugsFormViewModel viewModel = new BugsFormViewModel
            {
                Priorities = _context.Priorities.ToList()
            };

            return View("BugsForm", viewModel);
        }
        [Route("/bugs/edit/{id}")]
        public ActionResult Edit(int id)
        {
            Bug bug = _context.Bugs.Single(m => m.Id == id);
            if (bug == null)
            {
                return HttpNotFound("Bug not found");
            }

            BugsFormViewModel viewModel = new BugsFormViewModel(bug)
            {
                Priorities = _context.Priorities.ToList()
            };


            return View("BugsForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Bug bug)
        {
            if (!ModelState.IsValid)
            {
                BugsFormViewModel viewModel = new BugsFormViewModel(bug)
                {
                    Priorities = _context.Priorities.ToList()
                };
            }

            if (bug.Id == 0)
            {
                bug.DateCreated = DateTime.Now;
                bug.DateModified = DateTime.Now;
                _context.Bugs.Add(bug);
            }
            else
            {
                Bug movieInDb = _context.Bugs.Single(m => m.Id == bug.Id);

                movieInDb.Name = bug.Name;
                movieInDb.Details = bug.Details;
                movieInDb.PriorityId = bug.PriorityId;
                movieInDb.DateModified = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Bugs"); 
        }
    }
}