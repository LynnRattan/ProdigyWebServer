using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProdigyServerBL.Models;
using ProdigyServerBL.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProdigyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        ProdigyDbContext context;
        PenguinServices services;
        const string UserKey = "User";
        readonly JsonSerializerOptions options;

        public ValuesController(ProdigyDbContext context, PenguinServices services)
        {
            this.context = context;
            this.services = services;
            options = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        #region login + signup

        [Route("Login")] //works
        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            try
            {
                User u = context.GetUsersWithData().Where(x => x.UserPswd == user.UserPswd && x.Username == user.Username).FirstOrDefault();

                if (u != null)
                {
                    HttpContext.Session.SetObject(UserKey, u);
                    return Ok(u);
                }

                return Unauthorized();
            }
            catch (Exception ex) { return BadRequest(null); }
        }

        [Route("SignUp")] //works
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            if (context.GetUsersWithData().FirstOrDefault(u => u.Username == user.Username) != null)
                return Conflict();
            try
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                HttpContext.Session.SetObject(UserKey, user);
                return Ok(user);
            }
            catch (Exception)
            {
                throw new Exception("register error");
            }

        }

        #endregion

        #region change X

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
                User u1 = context.GetUsersWithData().FirstOrDefault(u => u.Id == user.Id);
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
                User u1 = context.GetUsersWithData().FirstOrDefault(u => u.Id == user.Id);
                u1.UserPswd = newPass;
                await context.SaveChangesAsync();
                return Ok(u1);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        #endregion


        [Route("AuthorBooks")]
        [HttpGet]
        public async Task<ActionResult<List<PenguinResult>>> BooksByAuthor(string name)
        {
            try
            {
                var userId = HttpContext.Session.GetObject<User>("user").Id;
                var a = await services.GetBookByAuthor(name);
                var favorites = context.UsersStarredBooks.Where(x => x.UserId == userId);

                List<PenguinResult> result = new List<PenguinResult>();
                foreach (var book in a)
                {
                    if (favorites.Any(x => x.BookIsbn == book.ISBN && x.UserId == userId))
                        result.Add(new PenguinResult(book) { IsStarred = true });
                    else
                        result.Add(book);
                }
                if (a.Count > 0)
                {
                    return Ok(result);
                }
                return NotFound();

            }
            catch (Exception)
            {
                return BadRequest(null);
            }
        }

        [Route("StarBook")] //if already starred, remove from UserStarredBooks. if not, add
        [HttpGet]
        public async Task<ActionResult> StarBook([FromQuery] string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return BadRequest();
            var userId = HttpContext.Session.GetObject<User>("user").Id;


            try
            {

                if (context.UsersStarredBooks.Where(x => x.UserId == userId && x.BookIsbn == isbn).AsNoTracking().FirstOrDefault() == null)
                {
                    context.UsersStarredBooks.Add(new UsersStarredBook() { BookIsbn = isbn, UserId = userId });
                    await context.SaveChangesAsync();
                }
                
                else
                {
                    context.UsersStarredBooks.Remove(context.UsersStarredBooks.Where(x => x.UserId == userId && x.BookIsbn == isbn).First());
                    await context.SaveChangesAsync(); 
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }


        [Route("TBRBook")]
        [HttpGet]
        public async Task<ActionResult> TBRBook([FromQuery] string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return BadRequest();
            var userId = HttpContext.Session.GetObject<User>("user").Id;

            try
            {

                if (context.UsersTBR.Where(x => x.UserId == userId && x.BookIsbn == isbn).AsNoTracking().FirstOrDefault() == null)
                {
                    context.UsersTBR.Add(new UsersTBR() { BookIsbn = isbn, UserId = userId });
                    await context.SaveChangesAsync();
                }

                else
                {
                    context.UsersTBR.Remove(context.UsersTBR.Where(x => x.UserId == userId && x.BookIsbn == isbn).First());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();


        }





        [Route("CRbook")]
        [HttpGet]
        public async Task<ActionResult> CurrentReadBook([FromQuery] string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return BadRequest();
            var userId = HttpContext.Session.GetObject<User>("user").Id;

            try
            {

                if (context.UsersCurrentRead.Where(x => x.UserId == userId && x.BookIsbn == isbn).AsNoTracking().FirstOrDefault() == null)
                {
                    context.UsersCurrentRead.Add(new UsersCurrentRead() { BookIsbn = isbn, UserId = userId });
                    await context.SaveChangesAsync();
                }

                else
                {
                    context.UsersCurrentRead.Remove(context.UsersCurrentRead.Where(x => x.UserId == userId && x.BookIsbn == isbn).First());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();


        }

        [Route("DroppedBook")]
        [HttpGet]
        public async Task<ActionResult> DroppedBook([FromQuery] string isbn)
        {
            if (string.IsNullOrEmpty(isbn)) return BadRequest();
            var userId = HttpContext.Session.GetObject<User>("user").Id;

            try
            {

                if (context.UsersDroppedBook.Where(x => x.UserId == userId && x.BookIsbn == isbn).AsNoTracking().FirstOrDefault() == null)
                {
                    context.UsersDroppedBook.Add(new UsersDroppedBook() { BookIsbn = isbn, UserId = userId });
                    await context.SaveChangesAsync();
                }

                else
                {
                    context.UsersDroppedBook.Remove(context.UsersDroppedBook.Where(x => x.UserId == userId && x.BookIsbn == isbn).First());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();


        }


        [Route(nameof(UploadImage))]
        [HttpPost]
        public async Task<ActionResult<User>> UploadImage([FromForm] IFormFile? file = null)
        {
            User? sessionUser = HttpContext.Session.GetObject<User>(UserKey);
            try
            {
                if (sessionUser == null || sessionUser.Id == null)
                    return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            if (file == null || file.Length == 0)
                return BadRequest();

            string fileName = $"{sessionUser.Id}{Path.GetExtension(file.FileName)}";

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            sessionUser.Image = fileName;
            context.Entry(sessionUser);
            context.SaveChanges();

            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))

                    file.CopyTo(fileStream);
                return Ok(sessionUser);
            }

            catch (Exception)
            {
                await context.SaveChangesAsync();
                return BadRequest();
            }
        }

    }
}


