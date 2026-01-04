using System.ComponentModel.DataAnnotations;

namespace GroupFundTracker.Api.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = "Member";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<MonthlyContribution> MonthlyContributions { get; set; }
            = new List<MonthlyContribution>();
    }
}
