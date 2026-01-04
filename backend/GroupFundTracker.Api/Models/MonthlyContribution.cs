using System.ComponentModel.DataAnnotations;

namespace GroupFundTracker.Api.Models
{
    public class MonthlyContribution
    {
        [Key]
        public int ContributionId { get; set; }

        public int MemberId { get; set; }

        public string ContributionMonth { get; set; } = null!; // 2025-08

        public decimal Amount { get; set; }

        public string? PaymentMode { get; set; }

        public string? Remarks { get; set; }

        public DateTime PaidDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Member Member { get; set; } = null!;

    }
}
