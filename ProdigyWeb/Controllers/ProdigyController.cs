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

        [Route("Login")]
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

        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] User user) 
        {
            if(context.Users.FirstOrDefault(u => u.Username == user.Username) != null)
                return Conflict();  
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync(); //doesnt work, doesnt have email in user. fix it bitch
            }
            catch (Exception)
            {
                throw new Exception("register error");
            }
            return Ok();
        }

    }
}
