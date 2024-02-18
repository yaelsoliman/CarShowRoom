using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SparkAuto.Data.Migrations
{
    public partial class AddServiceToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Miles = table.Column<double>(type: "float", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceShoppingCarts_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceShoppingCarts_serviceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "serviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicePrice = table.Column<double>(type: "float", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDetails_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDetails_serviceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "serviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceId",
                table: "ServiceDetails",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetails_ServiceTypeId",
                table: "ServiceDetails",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CarId",
                table: "Services",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceShoppingCarts_CarId",
                table: "ServiceShoppingCarts",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceShoppingCarts_ServiceTypeId",
                table: "ServiceShoppingCarts",
                column: "ServiceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceDetails");

            migrationBuilder.DropTable(
                name: "ServiceShoppingCarts");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
