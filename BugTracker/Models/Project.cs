using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Url { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        [StringLength(128)]
        public string UpdatedById { get; set; }
        public ApplicationUser UpdatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}