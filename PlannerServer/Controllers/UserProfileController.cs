using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PlannerServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

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
            string token = Request.Headers["Authorization"][0].Split(" ")[1];
            string userId = descript(token);
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
        [Authorize]
        [Route("AddTask")]
        //Post :/api/UserProfile/AddTask
        public async Task<Object> PostTask(TaskModel tm)
        {
            string token = Request.Headers["Authorization"][0].Split(" ")[1];
            string userId = descript(token);
            Model.Task t = new Model.Task();
            t.Comment = tm.Comment;
            t.Description = tm.Description;
            t.Title = tm.Title;
            t.StartDate = DateTime.Now;
            t.DueDate = t.StartDate.AddDays(tm.Days);
            t.Comment = "no comment";
            t.LeftDays = tm.Days;
            t.Status = true;
            t.Stats = "Progress";
            User us = new User();
            us = await _userManager.FindByIdAsync(userId);
            t.User = new User();
            t.User = us;
            t.UserID = us.Id;
            
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

        public String descript(String token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jwtToken.Claims.First(claim => claim.Type == "UserID").Value;
            return userId;
        }
        
        [HttpGet]
        [Authorize]
        [Route("GetTask")]
        //Get :/api/UserProfile/GetTask
        public async Task<Object> GetTasks()
        {
            string token = Request.Headers["Authorization"][0].Split(" ")[1];
            string userID = descript(token);
            List<Model.Task> tasks = new List<Model.Task>();
            tasks = _context.Tasks.ToList().Where(task => task.UserID == userID && task.Stats != "Yes" && task.Stats != "No").ToList();
            
            foreach (Model.Task task in tasks)
            {
                task.LeftDays = Convert.ToInt32( (task.DueDate - DateTime.Now).TotalDays);
                if (task.LeftDays <= 0 && task.Stats != "Yes")
                {
                    task.Stats = "No";
                    task.LeftDays = 0;
                    var result = _context.Tasks.SingleOrDefault(b => b.TaskId == task.TaskId);
                    if (result != null)
                    {
                        result.LeftDays = 0;
                        result.Stats = "No";
                        _context.SaveChanges();
                    }
                }

            }
        
            
            var jsonTask = JsonSerializer.Serialize(tasks);
            return jsonTask;
        }
        [HttpGet]
        [Authorize]
        [Route("GetFailedTask")]
        //Get :/api/UserProfile/GetTask
        public async Task<Object> GetFailedTasks()
        {
            string token = Request.Headers["Authorization"][0].Split(" ")[1];
            string userID = descript(token);
            List<Model.Task> tasks = new List<Model.Task>();
            tasks = _context.Tasks.ToList()
                .Where(task => task.UserID == userID && task.Stats == "No")
                .ToList();


            foreach (Model.Task task in tasks)
            {
                task.LeftDays = Convert.ToInt32((task.DueDate - DateTime.Now).TotalDays);
                if (task.LeftDays <= 0)
                {                   
                    task.LeftDays = 0;                   
                }

            }


            var jsonTask = JsonSerializer.Serialize(tasks);
            return jsonTask;
        }
        [HttpGet]
        [Authorize]
        [Route("GetAccTask")]
        //Get :/api/UserProfile/GetTask
        public async Task<Object> GetAccTasks()
        {
            string token = Request.Headers["Authorization"][0].Split(" ")[1];
            string userID = descript(token);
            List<Model.Task> tasks = new List<Model.Task>();
            tasks = _context.Tasks.ToList()
                .Where(task => task.UserID == userID && task.Stats == "Yes")
                .ToList();


            foreach (Model.Task task in tasks)
            {
                task.LeftDays = Convert.ToInt32((task.DueDate - DateTime.Now).TotalDays);
                if (task.LeftDays <= 0)
                {                   
                    task.LeftDays = 0;
                }
            }


            var jsonTask = JsonSerializer.Serialize(tasks);
            return jsonTask;
        }

        [HttpPost]
        [Authorize]
        [Route("GetTaskByID")]
        //Get :/api/UserProfile/GetTask
        public async Task<Object> GetTasksID(TaskModel tm)
        {
            Model.Task task = new Model.Task();
            task = (Model.Task)_context.Tasks.FirstOrDefault(t => t.TaskId == tm.TaskID);          
            var jsonTask = JsonSerializer.Serialize(task);
            return jsonTask;
        }

        [HttpPost]
        [Authorize]
        [Route("FailTask")]
        //Get :/api/UserProfile/FailTask
        public async Task<Object> SetFailTask(TaskModel tm)
        {
            var result = _context.Tasks.SingleOrDefault(b => b.TaskId == tm.TaskID);
            if (result != null)
            {
                result.LeftDays = 0;
                result.Stats = "No";
                _context.SaveChanges();
            }
            return Ok();
        }
        [HttpPost]
        [Authorize]
        [Route("SuccessTask")]
        //Get :/api/UserProfile/SuccessTask
        public async Task<Object> SetSuccessTask(TaskModel tm)
        {
            var result = _context.Tasks.SingleOrDefault(b => b.TaskId == tm.TaskID);
            if (result != null)
            {
                result.LeftDays = 0;
                result.Stats = "Yes";
                _context.SaveChanges();
            }
            return Ok();
        }


    }
}
