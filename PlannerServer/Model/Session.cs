using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class Session
    {
        private static Session instance;
        public static Session Instance
        {
            get
            {
                if (instance == null) instance = new Session();
                return instance;
            }
        }
        private Session() { }

        public User ConnectedUser { get; set; }
    }
}
