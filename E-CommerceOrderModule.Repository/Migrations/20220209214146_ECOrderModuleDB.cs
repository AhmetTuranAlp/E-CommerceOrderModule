using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_CommerceOrderModule.Repository.Migrations
{
    public partial class ECOrderModuleDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 41, 46, 40, DateTimeKind.Local).AddTicks(8588), new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(272) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8738), new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8756) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8821), new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8826) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8829), new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8832) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 13, 31, 861, DateTimeKind.Local).AddTicks(2810), new DateTime(2022, 2, 10, 0, 13, 31, 862, DateTimeKind.Local).AddTicks(2884) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(521), new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(535) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(579), new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(584) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "UpdateDate", "UploadDate" },
                values: new object[] { new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(586), new DateTime(2022, 2, 10, 0, 13, 31, 863, DateTimeKind.Local).AddTicks(589) });
        }
    }
}
