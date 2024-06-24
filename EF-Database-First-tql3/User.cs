using System;
using System.Collections.Generic;

namespace EF_Database_First_tql3;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public bool IsReviewer { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
