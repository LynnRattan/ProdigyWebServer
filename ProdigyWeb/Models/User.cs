using System;
using System.Collections.Generic;

namespace ProdigyWeb.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string UserPswd { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Image { get; set; }


}
