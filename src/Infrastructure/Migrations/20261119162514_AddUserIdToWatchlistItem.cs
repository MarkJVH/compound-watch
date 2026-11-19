using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompoundWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToWatchlistItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_watchlist_items_instrument_id",
                table: "watchlist_items");

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "watchlist_items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_watchlist_items_instrument_id",
                table: "watchlist_items",
                column: "instrument_id");

            migrationBuilder.CreateIndex(
                name: "ix_watchlist_items_user_id_instrument_id",
                table: "watchlist_items",
                columns: new[] { "user_id", "instrument_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_watchlist_items_instrument_id",
                table: "watchlist_items");

            migrationBuilder.DropIndex(
                name: "ix_watchlist_items_user_id_instrument_id",
                table: "watchlist_items");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "watchlist_items");

            migrationBuilder.CreateIndex(
                name: "ix_watchlist_items_instrument_id",
                table: "watchlist_items",
                column: "instrument_id",
                unique: true);
        }
    }
}
