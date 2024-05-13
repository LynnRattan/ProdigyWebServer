using System;
using System.Collections.Generic;

namespace ProdigyServerBL.Models;

public partial class UsersTbr
{
    public int Id { get; set; }

    public string BookIsbn { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
