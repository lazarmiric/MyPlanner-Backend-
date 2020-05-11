using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class User:IdentityUser
    {
        public int Age { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
       
        public List<Task> Tasks { get; set; }

    }
}
