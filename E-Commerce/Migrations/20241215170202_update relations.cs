using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class updaterelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Coupon_CouponId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CouponId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Prodeuct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prodeuct_CouponId",
                table: "Prodeuct",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodeuct_Coupon_CouponId",
                table: "Prodeuct",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodeuct_Coupon_CouponId",
                table: "Prodeuct");

            migrationBuilder.DropIndex(
                name: "IX_Prodeuct_CouponId",
                table: "Prodeuct");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Prodeuct");

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CouponId",
                table: "Order",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Coupon_CouponId",
                table: "Order",
                column: "CouponId",
                principalTable: "Coupon",
                principalColumn: "CouponId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
