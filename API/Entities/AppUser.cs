using System;
using Microsoft.Net.Http.Headers;

namespace API.Entities;

public class AppUser
{
    public required string Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Email { get; set; }

    public required byte[] PasswordHash { get; set; }

    public required byte[] PasswordSalt { get; set; }
}
