using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public String Password { get; set; }

        public int Age { get; set; }
    }
}
