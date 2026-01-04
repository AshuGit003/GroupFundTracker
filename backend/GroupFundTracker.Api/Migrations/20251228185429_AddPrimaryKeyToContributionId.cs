using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupFundTracker.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKeyToContributionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MonthlyContributions",
                newName: "ContributionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContributionId",
                table: "MonthlyContributions",
                newName: "Id");
        }
    }
}
