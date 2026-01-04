using GroupFundTracker.Api.Data;
using GroupFundTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroupFundTracker.Api.Controllers
{
    [ApiController]
    [Route("api/contributions")]
    public class ContributionsController : Controller
    {
        private readonly GroupFundDbContext _context;

        public ContributionsController(GroupFundDbContext context)
        {
            _context = context;
        }

        // POST: api/contributions
        [HttpPost]
        public async Task<IActionResult> AddContribution(MonthlyContribution contribution)
        {
            // Basic validation
            var memberExists = await _context.Members
                .AnyAsync(m => m.MemberId == contribution.MemberId && m.IsActive);

            if (!memberExists)
                return BadRequest("Invalid or inactive member.");

            _context.MonthlyContributions.Add(contribution);
            await _context.SaveChangesAsync();

            return Ok(contribution);
        }

        // GET: api/contributions/month/2025-08
        [HttpGet("month/{month}")]
        public async Task<IActionResult> GetContributionsByMonth(string month)
        {
            var contributions = await _context.MonthlyContributions
                .Include(c => c.Member)
                .Where(c => c.ContributionMonth == month)
                .OrderBy(c => c.PaidDate)
                .Select(c => new
                {
                    c.ContributionId,
                    c.ContributionMonth,
                    c.Amount,
                    c.PaidDate,
                    c.PaymentMode,
                    MemberName = c.Member.Name
                })
                .ToListAsync();

            return Ok(contributions);
        }

        // GET: api/contributions/member/1
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetMemberContributions(int memberId)
        {
            var contributions = await _context.MonthlyContributions
                .Where(c => c.MemberId == memberId)
                .OrderByDescending(c => c.PaidDate)
                .ToListAsync();

            return Ok(contributions);
        }

        // GET: api/contributions/summary/2025-08
        [HttpGet("summary/{month}")]
        public async Task<IActionResult> GetMonthlySummary(string month)
        {
            var summary = await _context.MonthlyContributions
                .Where(c => c.ContributionMonth == month)
                .GroupBy(c => c.ContributionMonth)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalAmount = g.Sum(x => x.Amount),
                    PaidCount = g.Count()
                })
                .FirstOrDefaultAsync();

            return Ok(summary);
        }
    }
}
