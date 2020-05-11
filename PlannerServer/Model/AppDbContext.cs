using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Model
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users {get; set;}
        public DbSet<Task> Tasks { get; set; }
    }
}
