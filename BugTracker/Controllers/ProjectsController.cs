using BugTracker.Models;
using BugTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

namespace BugTracker.Controllers
{
    public class ProjectsController : Controller
    {
        public ApplicationDbContext _context { get; set; }

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Projects
        public ActionResult Index()
        {
            //IEnumerable<Project> projects = _context.Projects.Include(p => p.CreatedBy).Include(p=>p.UpdatedBy).ToList();
            //return View(projects);
            return View();
        }

        // GET: /projects/details/{id}
        public ActionResult Details(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p=>p.Id == id);
            if(project == null)
            {
                return HttpNotFound("Project not found");
            }

            return View(project);
        }

        public ActionResult New()
        {
            ProjectsFormViewModel viewModel = new ProjectsFormViewModel
            {
                Users = _context.Users.ToList()
            };

            return View("ProjectsForm", viewModel);
        }

        [Route("/projects/edit/{id}")]
        public ActionResult Edit(int id)
        {
            Project project = _context.Projects.SingleOrDefault(p => p.Id == id);

            if(project == null)
            {
                return HttpNotFound("Project not found");
            }

            ProjectsFormViewModel viewModel = new ProjectsFormViewModel(project)
            {
                Users = _context.Users.ToList()
            };

            return View("ProjectsForm", viewModel);
        }

        public ActionResult Save(Project project)
        {
            if(!ModelState.IsValid)
            {
                ProjectsFormViewModel viewModel = new ProjectsFormViewModel()
                {
                    Users = _context.Users.ToList()
                };
            }

            if(project.Id == 0)
            {
                project.DateCreated = DateTime.Now;
                project.DateModified = DateTime.Now;
                project.CreatedBy = _context.Users.First();
                project.UpdatedBy = _context.Users.First();
                _context.Projects.Add(project);
            }
            else
            {
                Project projectInDb = _context.Projects.SingleOrDefault(p => p.Id == project.Id);
                if(projectInDb == null)
                {
                    return HttpNotFound("Project not found");
                }
                
                projectInDb.Name = project.Name;
                projectInDb.Description = project.Description;
                projectInDb.Url = project.Url;
                projectInDb.DateModified = DateTime.Now;

                string userId = User.Identity.GetUserId();
                ApplicationUser user =_context.Users.SingleOrDefault(u => u.Id == userId);

                if(user == null)
                {
                    return HttpNotFound("Invalid user");
                }
                projectInDb.UpdatedBy = user;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Projects");
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Project projectInDb = _context.Projects.SingleOrDefault(p => p.Id == id);
            if(projectInDb == null)
            {
                return;
            }

            _context.Projects.Remove(projectInDb);
            _context.SaveChanges();
        }
    }
}