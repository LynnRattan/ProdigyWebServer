using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdigyServerBL.Models;
using ProdigyServerBL.Services;

namespace ProdigyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ProdigyDbContext context;
        public ValuesController(ProdigyDbContext context)
        {
            this.context = context;
        }

        [Route("Login")] //works
        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            User u = context.Users.Where(x => x.UserPswd == user.UserPswd && x.Username == user.Username).FirstOrDefault();

            if (u != null)
            {
                HttpContext.Session.SetObject("user", u);
                return Ok(u);
            }

            return Unauthorized();
        }

        [Route("SignUp")] //works
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user) 
        {
            if(context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
                return Conflict();  
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync(); 
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception("register error");
            }
            
        }

        [Route("ChangeUsername")] //works
        [HttpPost]
        public async Task<ActionResult<User>> ChangeUsername([FromBody] User user, [FromQuery] string newUsername)
        {
            if (user == null)
                return BadRequest();

            if (context.Users.FirstOrDefault(u => u.Username == newUsername) != null)
                return Conflict();

            try
            {
                User u1 = context.Users.FirstOrDefault(u => u.Id == user.Id);
                u1.Username = newUsername;
                await context.SaveChangesAsync();
                return Ok(u1);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }
        [Route("ChangePassword")] 
        [HttpPost]  
        public async Task<ActionResult<User>> ChangePassword([FromBody] User user, [FromQuery] string newPass)
        {
            if (user == null)
                return BadRequest();

            if (context.Users.FirstOrDefault(u => u.UserPswd == newPass) != null)
                return Conflict();

            try
            {
                User u1 = context.Users.FirstOrDefault(u => u.Id == user.Id);
                u1.UserPswd = newPass;
                await context.SaveChangesAsync();
                return Ok(u1);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        //upload file
        [Route("UploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromQuery] int Id, IFormFile file)
        {

            User u = this.context.Users.Find(Id);


            //check file size
            if (file.Length > 0)
            {
                // Generate unique file name
                string fileName = $"{u.Id}{Path.GetExtension(file.FileName)}";

                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                try
                {
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    return Ok();
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            return BadRequest();
        }
    }
}
