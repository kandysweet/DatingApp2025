using API.Data;
using API.DTOs;
using API.Entities;

using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register(RegisterRequest request)
    {
        if (await EmailExists(request.Email)) return BadRequest("Email is already in use");
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            DisplayName = request.DisplayName,
            Email = request.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
            PasswordSalt = hmac.Key
        };
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user.ToDto(tokenService);
    }
}