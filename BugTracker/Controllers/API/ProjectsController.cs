using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugTracker.Controllers.API
{
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext _context { get; set; }

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/projects
        public IEnumerable<Project> GetProjects()
        {
            return _context.Projects.ToList();
        }

        // GET /api/project/{id}
        public Project GetProject(int id)
        {
            Project project =  _context.Projects.SingleOrDefault(p => p.Id == id);
            if(project == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return project;
        }

        // POST /api/projects
        [HttpPost]
        public Project CreateProject(Project project)
        {
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            string userId = User.Identity.GetUserId();
            ApplicationUser user = _context.Users.SingleOrDefault(u => u.Id == userId);
            
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            project.CreatedBy = user;
            project.DateCreated = DateTime.Now;
            project.DateModified = DateTime.Now;

            project.UpdatedBy = user;

            _context.Projects.Add(project);
            _context.SaveChanges();

            return project;
        }

        // PUT /api/projects/{id}
        [HttpPut]
        public void UpdateProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var projectInDb = _context.Projects.SingleOrDefault(p => p.Id == id);
            if(projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            projectInDb.Name = project.Name;
            projectInDb.Description = project.Description;
            projectInDb.Url = project.Url;
            projectInDb.UpdatedById = project.UpdatedById;
            projectInDb.DateModified = DateTime.Now;

            _context.SaveChanges();
        }

        // DELETE /api/projects/{id}
        [HttpDelete]
        public void DeleteProjects(int id)
        {
            var projectInDb = _context.Projects.SingleOrDefault(p => p.Id == id);
            if (projectInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Projects.Remove(projectInDb);
            _context.SaveChanges();
        }
    }
}
