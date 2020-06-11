using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }

        public bool Status { get; set; }

        public int Days { get; set; }

        public string Username { get; set; }
    }
}
