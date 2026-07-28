using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DJB_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProdANdOrderColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalPurchase",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "productids",
                table: "Orders",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "ProductID");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockAvailable",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockAvailable",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Orders",
                newName: "productids");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Orders",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "TotalPurchase",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");
        }
    }
}
