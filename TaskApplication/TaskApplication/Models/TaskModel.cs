using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskApplication.Models
{
    public class TaskModel
    {
        [Key]
        public int oldId { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string desc { get; set; }
    }
}