using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aryeo_Listing_Api.Migrations
{
    /// <inheritdoc />
    public partial class DBInitalize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Street_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postal_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    County_Or_Parish = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State_Or_Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State_Or_Province_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unparsed_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IS_Map_Dirty = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bedrooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bathrooms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Square_Feet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year_Built = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LotDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lot_Size_Acres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lot_Open_Parking_Spaces = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ListingDetails",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddressId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mls_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sub_Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Standard_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Large_Thumbnail_URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    List_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LotId = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<int>(type: "int", nullable: false),
                    Floor_Plans = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interactive_Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Downloads_Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListingDetails_AddressDetails_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingDetails_BuildingDetails_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "BuildingDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListingDetails_LotDetails_LotId",
                        column: x => x.LotId,
                        principalTable: "LotDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListingDetails_AddressId",
                table: "ListingDetails",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingDetails_BuildingId",
                table: "ListingDetails",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ListingDetails_LotId",
                table: "ListingDetails",
                column: "LotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListingDetails");

            migrationBuilder.DropTable(
                name: "AddressDetails");

            migrationBuilder.DropTable(
                name: "BuildingDetails");

            migrationBuilder.DropTable(
                name: "LotDetails");
        }
    }
}
