using Microsoft.EntityFrameworkCore.Migrations;

namespace TripsLog.Migrations
{
    public partial class seperateaddactivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripActivity_Activities_ActivityId",
                table: "TripActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_TripActivity_Trips_TripId",
                table: "TripActivity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripActivity",
                table: "TripActivity");

            migrationBuilder.RenameTable(
                name: "TripActivity",
                newName: "TripActivities");

            migrationBuilder.RenameIndex(
                name: "IX_TripActivity_ActivityId",
                table: "TripActivities",
                newName: "IX_TripActivities_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripActivities",
                table: "TripActivities",
                columns: new[] { "TripId", "ActivityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TripActivities_Activities_ActivityId",
                table: "TripActivities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripActivities_Trips_TripId",
                table: "TripActivities",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripActivities_Activities_ActivityId",
                table: "TripActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_TripActivities_Trips_TripId",
                table: "TripActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripActivities",
                table: "TripActivities");

            migrationBuilder.RenameTable(
                name: "TripActivities",
                newName: "TripActivity");

            migrationBuilder.RenameIndex(
                name: "IX_TripActivities_ActivityId",
                table: "TripActivity",
                newName: "IX_TripActivity_ActivityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripActivity",
                table: "TripActivity",
                columns: new[] { "TripId", "ActivityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TripActivity_Activities_ActivityId",
                table: "TripActivity",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripActivity_Trips_TripId",
                table: "TripActivity",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
