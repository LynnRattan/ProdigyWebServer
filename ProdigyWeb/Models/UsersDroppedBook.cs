using System;
using System.Collections.Generic;

namespace ProdigyWeb.Models;

public partial class UsersDroppedBook
{
    public int Id { get; set; }

    public string BookIsbn { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
