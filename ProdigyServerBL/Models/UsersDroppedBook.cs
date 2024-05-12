using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdigyServerBL.Models
{
    public partial class UsersDroppedBook
    {
        public int Id { get; set; }

        public string BookIsbn { get; set; } = null!;

        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
