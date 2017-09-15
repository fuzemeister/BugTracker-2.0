using BugTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.ViewModels
{
    public class BugsFormViewModel
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Details { get; set; }

        [DisplayName("Priority")]
        [Required]
        public int PriorityId { get; set; }

        
        public IEnumerable<Priority> Priorities { get; set; }

        public string Title
        {
            get
            {
                return Id != 0 ? "Edit bug" : "New bug";
            }
        }

        public BugsFormViewModel()
        {
            Id = 0;
        }

        public BugsFormViewModel(Bug bug)
        {
            Id = bug.Id;
            Name = bug.Name;
            Details = bug.Details;
            PriorityId = bug.PriorityId;
        }
    }
}