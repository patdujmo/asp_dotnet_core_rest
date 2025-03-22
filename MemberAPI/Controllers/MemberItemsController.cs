using MemberAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemberAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberItemsController : ControllerBase
{
  private readonly MemberContext _context;
  private readonly ILogger<MemberItemsController> _logger;

  public MemberItemsController(ILogger<MemberItemsController> logger, MemberContext context)
  {
    _logger = logger;
    _context = context;
  }

  // GET: api/memberitems
  [HttpGet]
  public async Task<ActionResult<IEnumerable<MemberDto>>> GetMemberItems() {
    return await _context.MemberItems
      .Select(member => MemberToDTO(member))
      .ToListAsync();
  }

  // GET: api/memberitems/{id}
  [HttpGet("{id}")]
  public async Task<ActionResult<MemberDto>> GetMemberItem(long id) {
    var memberItem = await _context.MemberItems.FindAsync(id);

    if(memberItem == null) {
      return NotFound();
    }
    return MemberToDTO(memberItem);
  }

  // POST: api/memberitems
  [HttpPost]
  public async Task<ActionResult<Member>> PostMemberItem(Member member)
  {
    _context.MemberItems.Add(member);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(GetMemberItems), new { id = member.Id }, member);
  }

  // DELETE: api/memberitems/{id}
  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteMemberItem(long id) {
    var memberItem = await _context.MemberItems.FindAsync(id);
    if(memberItem == null)
    {
      return NotFound();
    }
    _context.MemberItems.Remove(memberItem);
    await _context.SaveChangesAsync();
    return NoContent();
  }

  // PUT: api/memberitems/{id}
  [HttpPut("{id}")]
  public async Task<IActionResult> PutMemberItem(long id, MemberDto memberDto) {
    if (id != memberDto.Id) {
      return BadRequest();
    }
    var memberItem = await _context.MemberItems.FindAsync(id);
    if (memberItem == null) {
      return NotFound();
    }

    memberItem.FirstName = memberDto.FirstName;
    memberItem.LastName = memberDto.LastName;

    try {
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException) when (!MemberItemExists(id)) {
      return NotFound();
    }

    return NoContent();
  }

  private Boolean MemberItemExists(long id) {
    return _context.MemberItems.Any(e => e.Id == id);
  }

  private static MemberDto MemberToDTO(Member member) => new MemberDto {
    Id = member.Id,
    FirstName = member.FirstName,
    LastName = member.LastName
  };
}