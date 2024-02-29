using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyServerBL.Models
{
    public partial class ProdigyDbContext
    {
        public IEnumerable<User> GetUsersWithData()
        {
            return this.Users.Include(x => x.UsersStarredBooks);
        }
    }
}
