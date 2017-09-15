using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class ProjectsFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public string Title {
            get
            {
                if (Id == 0)
                { return "New project"; }
                else { return "Edit project"; }
            }
        }

        public ProjectsFormViewModel()
        {
            Id = 0;
        }

        public ProjectsFormViewModel(Project project)
        {
            Id = project.Id;
            Name = project.Name;
            Description = project.Description;
            Url = project.Url;
        }
    }
}