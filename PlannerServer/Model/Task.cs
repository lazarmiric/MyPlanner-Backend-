using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }

        public bool Status { get; set; }

        [ForeignKey("Id")]
        public String UserID { get; set; }
        public User User { get; set; }

        public int LeftDays { get; set; }
        public String Stats { get; set; }
    }
}
