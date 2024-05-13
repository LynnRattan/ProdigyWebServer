using System;
using System.Collections.Generic;

namespace ProdigyServerBL.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserPswd { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Image { get; set; }

    public virtual ICollection<UsersCurrentRead> UsersCurrentReads { get; set; } = new List<UsersCurrentRead>();

    public virtual ICollection<UsersDroppedBook> UsersDroppedBooks { get; set; } = new List<UsersDroppedBook>();

    public virtual ICollection<UsersStarredBook> UsersStarredBooks { get; set; } = new List<UsersStarredBook>();

    public virtual ICollection<UsersTBR> UsersTBRs { get; set; } = new List<UsersTBR>();
}
