using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AEspejo.FreightQuotes.Migrations.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Scac = table.Column<string>(type: "text", nullable: false),
                    IsMockMode = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConstantTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstantTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Constants",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConstantTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constants_ConstantTypes_ConstantTypeId",
                        column: x => x.ConstantTypeId,
                        principalTable: "ConstantTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accessorials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessorials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessorials_Constants_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Constants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrierSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CarrierId = table.Column<long>(type: "bigint", nullable: false),
                    SettingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CarrierSettingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrierSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrierSettings_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrierSettings_Constants_CarrierSettingTypeId",
                        column: x => x.CarrierSettingTypeId,
                        principalTable: "Constants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrierSettings_Constants_SettingTypeId",
                        column: x => x.SettingTypeId,
                        principalTable: "Constants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StateId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    Zip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Addresses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartyAddresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address1 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Address2 = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    StateId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<long>(type: "bigint", nullable: false),
                    Zip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CreatedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartyAddresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartyAddresses_Constants_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Constants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartyAddresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartyAddresses_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "CreatedUTC", "IsActive", "IsMockMode", "LastModifiedUTC", "Name", "Scac" },
                values: new object[,]
                {
                    { 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, true, null, "Fedex", "FXFE" },
                    { 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, true, null, "Estes", "EXLA" },
                    { 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, true, null, "UPS", "UPS" }
                });

            migrationBuilder.InsertData(
                table: "ConstantTypes",
                columns: new[] { "Id", "Code", "CreatedUTC", "IsActive", "LastModifiedUTC", "Name" },
                values: new object[,]
                {
                    { 1L, "ShippingUnits", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ShippingUnits" },
                    { 2L, "SubClass", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "SubClass" },
                    { 3L, "FreightClass", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "FreightClass" },
                    { 4L, "Accessorials", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Accessorials" },
                    { 5L, "PartyAddressType", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "PartyAddressType" },
                    { 6L, "EquipmentType", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "EquipmentType" },
                    { 7L, "SettingType", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "SettingType" },
                    { 8L, "CarrierSettingType", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "CarrierSettingType" },
                    { 9L, "Terms", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Terms" },
                    { 10L, "Role", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Role" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "CreatedUTC", "LastModifiedUTC", "Name" },
                values: new object[,]
                {
                    { 1L, "USA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "USA" },
                    { 2L, "CAN", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Canada" },
                    { 3L, "MEX", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Mexico" },
                    { 4L, "Other", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Constants",
                columns: new[] { "Id", "Code", "ConstantTypeId", "CreatedUTC", "IsActive", "LastModifiedUTC", "Name", "Order" },
                values: new object[,]
                {
                    { 1L, "Bags", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Bags", 0 },
                    { 2L, "Bales", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Bales", 0 },
                    { 3L, "Barrels", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Barrels", 0 },
                    { 4L, "BasePlate", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Base Plate", 0 },
                    { 5L, "Baskets", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Baskets", 0 },
                    { 6L, "Boxes", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Boxes", 0 },
                    { 7L, "Buckets", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Buckets", 0 },
                    { 8L, "Bulkheads", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Bulkheads", 0 },
                    { 9L, "Bundles", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Bundles", 0 },
                    { 10L, "Carboys", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Carboys", 0 },
                    { 11L, "Carrier", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Carrier", 0 },
                    { 12L, "Cartons", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Cartons", 0 },
                    { 13L, "Carts", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Carts", 0 },
                    { 14L, "Cases", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Cases", 0 },
                    { 15L, "Coils", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Coils", 0 },
                    { 16L, "Crate", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Crate", 0 },
                    { 17L, "Drums", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Drums", 0 },
                    { 18L, "Eaches", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Eaches", 0 },
                    { 19L, "EmptyContainers", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Empty Containers", 0 },
                    { 20L, "EmptyDrums", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Empty Drums", 0 },
                    { 21L, "EmptyTotes", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Empty Totes", 0 },
                    { 22L, "Feet", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Feet", 0 },
                    { 23L, "Firkins", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Firkins", 0 },
                    { 24L, "Gaylords", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Gaylords", 0 },
                    { 25L, "Hampers", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Hampers", 0 },
                    { 26L, "Hogsheads", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Hogsheads", 0 },
                    { 27L, "Kegs", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Kegs", 0 },
                    { 28L, "Models", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Models", 0 },
                    { 29L, "Packages", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Packages", 0 },
                    { 30L, "Pails", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Pails", 0 },
                    { 31L, "Pallets", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Pallets", 0 },
                    { 32L, "Pieces", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Pieces", 0 },
                    { 33L, "Racks", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Racks", 0 },
                    { 34L, "Reels", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Reels", 0 },
                    { 35L, "Rolls", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Rolls", 0 },
                    { 36L, "Skid", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Skid", 0 },
                    { 37L, "SlipSheets", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Slip Sheets", 0 },
                    { 38L, "Sows", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Sows", 0 },
                    { 39L, "SuperSack", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Super Sack", 0 },
                    { 40L, "Tanks", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Tanks", 0 },
                    { 41L, "Totes", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Totes", 0 },
                    { 42L, "Trunks", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Trunks", 0 },
                    { 43L, "Tubes", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Tubes", 0 },
                    { 44L, "Unpackaged", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Unpackaged", 0 },
                    { 45L, "1", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "1", 1 },
                    { 46L, "2", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "2", 2 },
                    { 47L, "3", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "3", 3 },
                    { 48L, "4", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "4", 4 },
                    { 49L, "5", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "5", 5 },
                    { 50L, "6", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "6", 6 },
                    { 51L, "7", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "7", 7 },
                    { 52L, "8", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "8", 8 },
                    { 53L, "9", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "9", 9 },
                    { 54L, "10", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "10", 10 },
                    { 55L, "11", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "11", 11 },
                    { 56L, "12", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "12", 12 },
                    { 57L, "13", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "13", 13 },
                    { 58L, "14", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "14", 14 },
                    { 59L, "15", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "15", 15 },
                    { 60L, "16", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "16", 16 },
                    { 61L, "17", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "17", 17 },
                    { 62L, "18", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "18", 18 },
                    { 63L, "19", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "19", 19 },
                    { 64L, "20", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "20", 20 },
                    { 65L, "50", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "50", 1 },
                    { 66L, "55", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "55", 2 },
                    { 67L, "60", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "60", 3 },
                    { 68L, "65", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "65", 4 },
                    { 69L, "70", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "70", 5 },
                    { 70L, "77.5", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "77.5", 6 },
                    { 71L, "85", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "85", 7 },
                    { 72L, "92.5", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "92.5", 8 },
                    { 73L, "100", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "100", 9 },
                    { 74L, "110", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "110", 10 },
                    { 75L, "125", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "125", 11 },
                    { 76L, "150", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "150", 12 },
                    { 77L, "175", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "175", 13 },
                    { 78L, "200", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "200", 14 },
                    { 79L, "250", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "250", 15 },
                    { 80L, "300", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "300", 16 },
                    { 81L, "400", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "400", 17 },
                    { 82L, "500", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "500", 18 },
                    { 83L, "General", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "General", 0 },
                    { 84L, "DockType", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Dock Type", 0 },
                    { 85L, "OD", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Origin and Destination", 0 },
                    { 86L, "Item", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Item", 0 },
                    { 87L, "Other", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Other", 0 },
                    { 88L, "B", 5L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Billing", 0 },
                    { 89L, "O", 5L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Origin", 0 },
                    { 90L, "D", 5L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Destination", 0 },
                    { 91L, "DryVan", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Dry Van", 1 },
                    { 92L, "Reefer", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Reefer", 2 },
                    { 93L, "Flatbed", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Flatbed", 3 },
                    { 94L, "StepDeck", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Step Deck", 4 },
                    { 95L, "StraightTruck", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Straight Truck", 5 },
                    { 96L, "SprinterVan", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Sprinter Van", 6 },
                    { 97L, "BoxTruck", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Box Truck", 7 },
                    { 98L, "PowerOnly", 6L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Power Only", 8 },
                    { 99L, "Rating", 7L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Rating", 1 },
                    { 100L, "Authentication", 7L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Authentication", 2 },
                    { 101L, "URL", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "URL", 1 },
                    { 102L, "ClientId", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ClientId", 2 },
                    { 103L, "ClientSecret", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ClientSecret", 3 },
                    { 104L, "UserName", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "UserName", 4 },
                    { 105L, "Password", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Password", 5 },
                    { 106L, "ApiKey", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "ApiKey", 6 },
                    { 107L, "Account", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Account", 7 },
                    { 108L, "AccountSecundary", 8L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "AccountSecundary", 8 },
                    { 109L, "Collect", 9L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Collect", 1 },
                    { 110L, "Prepaid", 9L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Prepaid", 2 },
                    { 111L, "ThirdParty", 9L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Third Party", 3 },
                    { 112L, "Consignee", 10L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Consignee", 1 },
                    { 113L, "Shipper", 10L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Shipper", 2 },
                    { 114L, "ThirdParty", 10L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Third Party", 3 }
                });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "Id", "Code", "CountryId", "CreatedUTC", "LastModifiedUTC", "Name" },
                values: new object[,]
                {
                    { 1L, "AL", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Alabama" },
                    { 2L, "AK", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Alaska" },
                    { 3L, "AZ", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Arizona" },
                    { 4L, "AR", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Arkansas" },
                    { 5L, "CA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "California" },
                    { 6L, "CO", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Colorado" },
                    { 7L, "CT", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Connecticut" },
                    { 8L, "DE", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Delaware" },
                    { 9L, "FL", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Florida" },
                    { 10L, "GA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Georgia" },
                    { 11L, "HI", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hawaii" },
                    { 12L, "ID", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Idaho" },
                    { 13L, "IL", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Illinois" },
                    { 14L, "IN", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Indiana" },
                    { 15L, "IA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Iowa" },
                    { 16L, "KS", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Kansas" },
                    { 17L, "KY", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Kentucky" },
                    { 18L, "LA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Louisiana" },
                    { 19L, "ME", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Maine" },
                    { 20L, "MD", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Maryland" },
                    { 21L, "MA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Massachusetts" },
                    { 22L, "MI", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Michigan" },
                    { 23L, "MN", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Minnesota" },
                    { 24L, "MS", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Mississippi" },
                    { 25L, "MO", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Missouri" },
                    { 26L, "MT", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Montana" },
                    { 27L, "NE", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nebraska" },
                    { 28L, "NV", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nevada" },
                    { 29L, "NH", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "New Hampshire" },
                    { 30L, "NJ", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "New Jersey" },
                    { 31L, "NM", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "New Mexico" },
                    { 32L, "NY", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "New York" },
                    { 33L, "NC", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "North Carolina" },
                    { 34L, "ND", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "North Dakota" },
                    { 35L, "OH", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Ohio" },
                    { 36L, "OK", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Oklahoma" },
                    { 37L, "OR", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Oregon" },
                    { 38L, "PA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pennsylvania" },
                    { 39L, "PR", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Puerto Rico" },
                    { 40L, "RI", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Rhode Island" },
                    { 41L, "SC", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "South Carolina" },
                    { 42L, "SD", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "South Dakota" },
                    { 43L, "TN", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tennessee" },
                    { 44L, "TX", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Texas" },
                    { 45L, "UT", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Utah" },
                    { 46L, "VT", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vermont" },
                    { 47L, "VA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Virginia" },
                    { 48L, "WA", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Washington" },
                    { 49L, "DC", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Washington DC" },
                    { 50L, "WV", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "West Virginia" },
                    { 51L, "WI", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wisconsin" },
                    { 52L, "WY", 1L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Wyoming" },
                    { 53L, "AB", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Alberta" },
                    { 54L, "CC", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Canada Cross Border" },
                    { 55L, "CN", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Canada Intra" },
                    { 56L, "BC", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Colombie Britannique" },
                    { 57L, "PE", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Île du Prince Édouard" },
                    { 58L, "MB", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Manitoba" },
                    { 59L, "NB", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nouveau Brunswick" },
                    { 60L, "NS", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nouvelle Écosse" },
                    { 61L, "NU", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nunavut" },
                    { 62L, "ON", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Ontario" },
                    { 63L, "QC", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Québec" },
                    { 64L, "SK", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Saskatchewan" },
                    { 65L, "NL", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Terre Neuve et Labrador" },
                    { 66L, "NT", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Territoires du Nord Ouest" },
                    { 67L, "YT", 2L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Yukon" },
                    { 68L, "Ags", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Aguascalientes" },
                    { 69L, "BC", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Baja California" },
                    { 70L, "BCS", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Baja California Sur" },
                    { 71L, "Camp", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Campeche" },
                    { 72L, "CHIS", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chiapas" },
                    { 73L, "CHIH", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Chihuahua" },
                    { 74L, "COAH", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Coahuila" },
                    { 75L, "COL", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Colima" },
                    { 76L, "CDMX", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Ciudad de México" },
                    { 77L, "DGO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Durango" },
                    { 78L, "GTO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Guanajuato" },
                    { 79L, "GRO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Guerrero" },
                    { 80L, "HGO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hidalgo" },
                    { 81L, "JAL", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Jalisco" },
                    { 82L, "MEX", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Estado de México" },
                    { 83L, "MICH", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Michoacán" },
                    { 84L, "MOR", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Morelos" },
                    { 85L, "NAY", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nayarit" },
                    { 86L, "NL", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Nuevo León" },
                    { 87L, "OAX", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Oaxaca" },
                    { 88L, "PUE", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Puebla" },
                    { 89L, "QRO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Querétaro" },
                    { 90L, "QROO", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Quintana Roo" },
                    { 91L, "SLP", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "San Luis Potosí" },
                    { 92L, "SIN", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sinaloa" },
                    { 93L, "SON", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sonora" },
                    { 94L, "TAB", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tabasco" },
                    { 95L, "Tamps", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tamaulipas" },
                    { 96L, "TLAX", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tlaxcala" },
                    { 97L, "VER", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Veracruz" },
                    { 98L, "YUC", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Yucatán" },
                    { 99L, "ZAC", 3L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Zacatecas" },
                    { 100L, "Other", 4L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other" }
                });

            migrationBuilder.InsertData(
                table: "Accessorials",
                columns: new[] { "Id", "Code", "CreatedUTC", "IsActive", "LastModifiedUTC", "Name", "TypeId" },
                values: new object[,]
                {
                    { 1L, "BLS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "BlindShipment", 83L },
                    { 2L, "INS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Insurance", 83L },
                    { 3L, "PFF", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Protect From Freeze", 83L },
                    { 4L, "SAS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Sort and Segregate", 83L },
                    { 5L, "TRS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "TradeShow", 83L },
                    { 6L, "AIR", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Airport", 84L },
                    { 7L, "BUS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Business", 84L },
                    { 8L, "CHU", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Church", 84L },
                    { 9L, "CNS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Construction Site", 84L },
                    { 10L, "FAR", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Farm", 84L },
                    { 11L, "GOV", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Government", 84L },
                    { 12L, "GRW", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Grocery Warehouse", 84L },
                    { 13L, "HOT", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Hotel", 84L },
                    { 14L, "LAC", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "LimitedAccess", 84L },
                    { 15L, "MIL", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Military Site", 84L },
                    { 16L, "MIN", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Mine", 84L },
                    { 17L, "NUC", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Nuclear", 84L },
                    { 18L, "OLA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Other Limited Access", 84L },
                    { 19L, "PRI", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Prison", 84L },
                    { 20L, "RES", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Residential", 84L },
                    { 21L, "SCH", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "School", 84L },
                    { 22L, "STO", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Storage Facility", 84L },
                    { 23L, "APP", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Appointment", 85L },
                    { 24L, "INP", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Inside", 85L },
                    { 25L, "LFG", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "LiftGate", 85L },
                    { 26L, "NTF", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Notification", 85L },
                    { 27L, "HAZ", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "HazMat", 86L },
                    { 28L, "OVL", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "OverLength", 86L },
                    { 29L, "HST", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "HST", 87L },
                    { 30L, "PST", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "PST", 87L },
                    { 31L, "XBF", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "X border fee", 87L },
                    { 32L, "HCL", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "High cost lane", 87L },
                    { 33L, "DTD", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Detention Destination", 87L },
                    { 34L, "DTO", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Detention Origin", 87L },
                    { 35L, "LAY", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "LayOver", 87L },
                    { 36L, "LUM", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Lumper Service", 87L },
                    { 37L, "OTH", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Other", 87L },
                    { 38L, "REC", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Reconsignment", 87L },
                    { 39L, "TDA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Tailgating - Driver Assist", 87L },
                    { 40L, "TPC", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Temperature Control", 87L },
                    { 41L, "TON", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "TONU", 87L },
                    { 42L, "WIF", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "W&I Fee", 87L },
                    { 43L, "ADS", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Additional Stops", 87L },
                    { 44L, "FMO", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "Show Final Mile Options", 87L },
                    { 45L, "COD", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "COD", 83L },
                    { 46L, "INB", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "In Bond", 83L }
                });

            migrationBuilder.InsertData(
                table: "CarrierSettings",
                columns: new[] { "Id", "CarrierId", "CarrierSettingTypeId", "CreatedUTC", "IsActive", "LastModifiedUTC", "SettingTypeId", "Value" },
                values: new object[,]
                {
                    { 1L, 1L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 99L, "" },
                    { 2L, 1L, 107L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 99L, "" },
                    { 3L, 1L, 108L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 99L, "" },
                    { 4L, 1L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 5L, 1L, 102L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 6L, 1L, 103L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 7L, 2L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 99L, "" },
                    { 8L, 2L, 106L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 99L, "" },
                    { 9L, 2L, 101L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 10L, 2L, 106L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 11L, 2L, 104L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" },
                    { 12L, 2L, 105L, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, 100L, "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accessorials_TypeId",
                table: "Accessorials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StateId",
                table: "Addresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierSettings_CarrierId_SettingTypeId_CarrierSettingTypeId",
                table: "CarrierSettings",
                columns: new[] { "CarrierId", "SettingTypeId", "CarrierSettingTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarrierSettings_CarrierSettingTypeId",
                table: "CarrierSettings",
                column: "CarrierSettingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierSettings_SettingTypeId",
                table: "CarrierSettings",
                column: "SettingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Constants_ConstantTypeId",
                table: "Constants",
                column: "ConstantTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddresses_CountryId",
                table: "PartyAddresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddresses_StateId",
                table: "PartyAddresses",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PartyAddresses_TypeId",
                table: "PartyAddresses",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessorials");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CarrierSettings");

            migrationBuilder.DropTable(
                name: "PartyAddresses");

            migrationBuilder.DropTable(
                name: "Carriers");

            migrationBuilder.DropTable(
                name: "Constants");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "ConstantTypes");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
