using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdigyServerBL.Models;

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

        [Route("ChangeUsername")] 
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

    }
}
