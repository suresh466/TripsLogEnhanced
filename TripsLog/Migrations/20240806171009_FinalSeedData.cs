using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TripsLog.Migrations
{
    public partial class FinalSeedData : Migration
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

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "contact@hoteltaj.com", "Hotel Taj" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "info@hotelopera.com", "Hotel Opera" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Visit Taj Mahal");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Go To India Gate");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Visit Pokhara");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Climb Mt Everest");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Go Trekking");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "India");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Nepal");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Nepal");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "contact@hotelparis.com", "Hotel Paris" });

            migrationBuilder.UpdateData(
                table: "Accommodations",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "info@tokyoinn.com", "Tokyo Inn" });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Visit Eiffel Tower");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Explore Louvre Museum");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "See Statue of Liberty");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Visit Central Park");

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Explore Times Square");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Paris");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Tokyo");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "New York");

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
