using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Agregado para ToListAsync
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
namespace API.Controllers;

public class AccountController(AppDbContext context) : BaseAPIController
{

  [HttpPost("register")]
  public async Task<ActionResult<AppUser>> Register(RegisterRequest request)
  {
    using var hmac = new HMACSHA512();
    var user = new AppUser
    {
      Email = request.Email.ToLower(),
      DisplayName = request.DisplayName,
      PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
      PasswordSalt = hmac.Key
    };

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return user;
  }
}