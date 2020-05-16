using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlannerServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<User> _userManager;
        private AppDbContext _context;

        public UserProfileController(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        //GET :/api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new 
            {
                user.FirstName,
                user.LastName,
                user.Age,
                user.UserName
            };
        }
        [HttpPost]
        
        [Route("AddTask")]
        //Post :/api/UserProfile/AddTask
        public async Task<Object> PostTask(TaskModel tm)
        {
            Model.Task t = new Model.Task();
            t.Comment = tm.Comment;
            t.Description = tm.Description;
            t.Title = tm.Title;
            t.StartDate = DateTime.Now;
            t.DueDate = t.StartDate.AddDays(tm.Days);
            t.Comment = "no comment";
            t.Status = true;
            User us = new User();
            us = await _userManager.FindByNameAsync(tm.Username);
            t.User = new User();
            t.User = us;
            
            try
            {
                _context.Add(t);
                var result = await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
