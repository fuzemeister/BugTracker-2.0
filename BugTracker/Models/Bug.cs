using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models
{
    public class Bug
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Details { get; set; }

        [DisplayName("Priority")]
        public int PriorityId { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [DisplayName("Date created")]
        [Required]
        public DateTime DateCreated { get; set; }

        [DisplayName("Date modified")]
        [Required]
        public DateTime DateModified { get; set; }
    }
}