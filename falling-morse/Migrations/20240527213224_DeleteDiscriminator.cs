using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pv178_project.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ScoreResults");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ScoreResults",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
