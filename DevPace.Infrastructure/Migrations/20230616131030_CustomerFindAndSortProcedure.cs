using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevPace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerFindAndSortProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"CREATE OR ALTER PROCEDURE [dbo].[CustomerFindAndSortProcedure]
                    @SearchItem NVARCHAR(100),
                    @OrderBy NVARCHAR(100),
                    @OrderDescending BIT,
                    @RowCount INT,
                    @PageNumber INT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    SELECT*FROM dbo.Customers
                    WHERE 
                        CONCAT(Name, CompanyName, Email, PhoneNumber) LIKE @SearchItem
                    ORDER BY
                        CASE WHEN @OrderBy = 'Name' AND @OrderDescending = 0 THEN Name END,
                        CASE WHEN @OrderBy = 'Name' AND @OrderDescending = 1 THEN Name END DESC,
                        CASE WHEN @OrderBy = 'CompanyName' AND @OrderDescending = 0 THEN CompanyName END,
                        CASE WHEN @OrderBy = 'CompanyName' AND @OrderDescending = 1 THEN CompanyName END DESC,
                        CASE WHEN @OrderBy = 'Email' AND @OrderDescending = 0 THEN Email END,
                        CASE WHEN @OrderBy = 'Email' AND @OrderDescending = 1 THEN Email END DESC,
                        CASE WHEN @OrderBy = 'PhoneNumber' AND @OrderDescending = 0 THEN PhoneNumber END,
                        CASE WHEN @OrderBy = 'PhoneNumber' AND @OrderDescending = 1 THEN PhoneNumber END DESC
                    OFFSET (@PageNumber - 1) * @RowCount ROWS
                    FETCH NEXT @RowCount ROWS ONLY;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE[dbo].[CustomerFindAndSortProcedure]");
        }
    }
}
