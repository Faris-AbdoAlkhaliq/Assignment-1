using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EyeCare.Data.Migrations
{
    /// <inheritdoc />
    public partial class InisialSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eyes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenTimeHoursPerDay = table.Column<int>(type: "int", nullable: false),
                    GlassesType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasEyeDisease = table.Column<bool>(type: "bit", nullable: false),
                    EyeDiseaseDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamilyHistoryOfEyeDisease = table.Column<bool>(type: "bit", nullable: false),
                    PastEyeSurgeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperiencesEyeStrain = table.Column<bool>(type: "bit", nullable: false),
                    ExperiencesDryEyes = table.Column<bool>(type: "bit", nullable: false),
                    BlurredVision = table.Column<bool>(type: "bit", nullable: false),
                    Smokes = table.Column<bool>(type: "bit", nullable: false),
                    WearsSunglasses = table.Column<bool>(type: "bit", nullable: false),
                    LastEyeCheckupInMonths = table.Column<int>(type: "int", nullable: false),
                    UsesEyeDrops = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eyes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eyes");
        }
    }
}
