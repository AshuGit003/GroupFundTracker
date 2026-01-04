using GroupFundTracker.Api.Data;
using GroupFundTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupFundTracker.Api.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MembersController : Controller
    {
        private readonly GroupFundDbContext _context;
        public MembersController(GroupFundDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        public async Task<IActionResult> GetMembers() 
        { 
            var members = await _context.Members
                .Where(m => m.IsActive)
                .OrderBy(m => m.Name)
                .ToListAsync();

            return Ok(members);
        }

        // GET: api/members/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null || !member.IsActive)
                return NotFound();

            return Ok(member);
        }

        // POST: api/members
        [HttpPost]
        public async Task<IActionResult> AddMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMember),
                new { id = member.MemberId }, member);
        }

        // PUT: api/members/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(int id, Member updatedMember)
        {
            if (id != updatedMember.MemberId)
                return BadRequest();

            var member = await _context.Members.FindAsync(id);

            if (member == null)
                return NotFound();

            member.Name = updatedMember.Name;
            member.Role = updatedMember.Role;
            member.IsActive = updatedMember.IsActive;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
