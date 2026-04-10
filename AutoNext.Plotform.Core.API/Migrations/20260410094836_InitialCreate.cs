using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutoNext.Plotform.Core.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    country_code = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    state_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    city_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    district = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    pincode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "city_areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    area_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    pincode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_city_areas_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_city_areas_location_id",
                table: "city_areas",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_CityArea_Pincode",
                table: "city_areas",
                column: "pincode");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Country_State_City",
                table: "locations",
                columns: new[] { "country_code", "state_code", "city_name" });

            migrationBuilder.CreateIndex(
                name: "IX_Location_Pincode",
                table: "locations",
                column: "pincode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_areas");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
