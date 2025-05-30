﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WEB_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ccRIACat_Areas",
                columns: table => new
                {
                    IDArea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusArea = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ccRIACat_Areas", x => x.IDArea);
                });

            migrationBuilder.CreateTable(
                name: "ccUsers",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoUser_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    fCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDArea = table.Column<int>(type: "int", nullable: false),
                    LastLoginAttempt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ccUsers", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_ccUsers_ccRIACat_Areas_IDArea",
                        column: x => x.IDArea,
                        principalTable: "ccRIACat_Areas",
                        principalColumn: "IDArea",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ccloglogin",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Extension = table.Column<int>(type: "int", nullable: false),
                    TipoMov = table.Column<int>(type: "int", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ccloglogin", x => x.id);
                    table.ForeignKey(
                        name: "FK_ccloglogin_ccUsers_User_id",
                        column: x => x.User_id,
                        principalTable: "ccUsers",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ccloglogin_User_id",
                table: "ccloglogin",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_ccUsers_IDArea",
                table: "ccUsers",
                column: "IDArea");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ccloglogin");

            migrationBuilder.DropTable(
                name: "ccUsers");

            migrationBuilder.DropTable(
                name: "ccRIACat_Areas");
        }
    }
}
