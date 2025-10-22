using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Agregado para ToListAsync
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;

namespace API.Controllers;

[Authorize]
public class MembersController(IMembersRepository membersRepository) : BaseAPIController
{
  [HttpGet]
  public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers()
  {
    var members = await membersRepository.GetMembersAsync();

    return Ok(members);
  }

  [AllowAnonymous]
  [HttpGet("{id}")] // https://localhost:5001/api/members/bob-id
  public async Task<ActionResult<Member>> GetMember(string id)
  {
    var member = await membersRepository.GetMemberAsync(id);

    if (member == null) return NotFound();

    return Ok(member);
  }
}