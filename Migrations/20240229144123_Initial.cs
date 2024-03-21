using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace task.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingMaterials",
                columns: table => new
                {
                    MaterialCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaterialName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingMaterials", x => x.MaterialCode);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.DistrictCode);
                });

            migrationBuilder.CreateTable(
                name: "EvaluationCriteria",
                columns: table => new
                {
                    CriterionCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CriterionName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationCriteria", x => x.CriterionCode);
                });

            migrationBuilder.CreateTable(
                name: "Realtors",
                columns: table => new
                {
                    RealtorCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    ContactPhone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realtors", x => x.RealtorCode);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateObjects",
                columns: table => new
                {
                    ObjectCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DistrictCode = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    RoomCount = table.Column<int>(type: "integer", nullable: false),
                    TypeCode = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    MaterialCode = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    ListingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateObjects", x => x.ObjectCode);
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_BuildingMaterials_MaterialCode",
                        column: x => x.MaterialCode,
                        principalTable: "BuildingMaterials",
                        principalColumn: "MaterialCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_Districts_DistrictCode",
                        column: x => x.DistrictCode,
                        principalTable: "Districts",
                        principalColumn: "DistrictCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateObjects_Types_TypeCode",
                        column: x => x.TypeCode,
                        principalTable: "Types",
                        principalColumn: "TypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectCode = table.Column<int>(type: "integer", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CriterionCode = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationCode);
                    table.ForeignKey(
                        name: "FK_Evaluations_EvaluationCriteria_CriterionCode",
                        column: x => x.CriterionCode,
                        principalTable: "EvaluationCriteria",
                        principalColumn: "CriterionCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_RealEstateObjects_ObjectCode",
                        column: x => x.ObjectCode,
                        principalTable: "RealEstateObjects",
                        principalColumn: "ObjectCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleCode = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ObjectCode = table.Column<int>(type: "integer", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RealtorCode = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleCode);
                    table.ForeignKey(
                        name: "FK_Sales_RealEstateObjects_ObjectCode",
                        column: x => x.ObjectCode,
                        principalTable: "RealEstateObjects",
                        principalColumn: "ObjectCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Realtors_RealtorCode",
                        column: x => x.RealtorCode,
                        principalTable: "Realtors",
                        principalColumn: "RealtorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_CriterionCode",
                table: "Evaluations",
                column: "CriterionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ObjectCode",
                table: "Evaluations",
                column: "ObjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_DistrictCode",
                table: "RealEstateObjects",
                column: "DistrictCode");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_MaterialCode",
                table: "RealEstateObjects",
                column: "MaterialCode");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateObjects_TypeCode",
                table: "RealEstateObjects",
                column: "TypeCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ObjectCode",
                table: "Sales",
                column: "ObjectCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_RealtorCode",
                table: "Sales",
                column: "RealtorCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "EvaluationCriteria");

            migrationBuilder.DropTable(
                name: "RealEstateObjects");

            migrationBuilder.DropTable(
                name: "Realtors");

            migrationBuilder.DropTable(
                name: "BuildingMaterials");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
