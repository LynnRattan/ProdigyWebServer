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
            User u = context.Users.Where(x => x.UserPswd == user.UserPswd && x.Email == user.Email).FirstOrDefault();

            if (u != null)
            {
                HttpContext.Session.SetObject("user", u);
                return Ok(u);
            }

            return Unauthorized();
        }

        [Route("Hello")]
        [HttpGet]
        public async Task<ActionResult> Hello()
        {
            return Ok("hi");
        }

    }
}
