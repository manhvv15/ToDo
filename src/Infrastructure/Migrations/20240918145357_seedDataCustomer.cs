using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedDataCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0996cf33-6b9b-407d-be5e-335a992661a5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("0e4d0def-55ef-4673-be2b-c1aa858b3a0f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("31eb3c59-d7db-4465-8c6b-1c59c54acdf2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6fd4cd16-4bf4-43f0-bfbd-270421271eaa"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ea0bc2f4-269c-4779-bb8d-d55bfea6ee8b"));

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Created", "CreatedBy", "LastModified", "LastModifiedBy", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("3e1b0512-7046-4369-8ec0-38e1224fabc1"), "Ha Noi", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Tri", "0353171947" },
                    { new Guid("950bdef2-1ab1-41c7-be60-435dd030f323"), "Nam Dinh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Manh", "0353271947" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Created", "CreatedBy", "Detail", "LastModified", "LastModifiedBy", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("150aeacd-1db8-4e01-9302-0beebcb67864"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 11", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 11", 19.989999999999998, 10 },
                    { new Guid("2f86287a-4c97-48eb-9d91-8c18dda57bf2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 14", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 14", 45.0, 7 },
                    { new Guid("72f380ce-a322-47b9-a8de-3c00e2fd834f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 13", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 13", 15.5, 20 },
                    { new Guid("7fc8cc8c-78fa-493f-909c-2b02ca0f848a"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 12", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 12", 29.989999999999998, 5 },
                    { new Guid("ed7e1efb-988d-4008-8cce-22c564eed336"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 15", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 15", 9.9900000000000002, 30 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("3e1b0512-7046-4369-8ec0-38e1224fabc1"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("950bdef2-1ab1-41c7-be60-435dd030f323"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("150aeacd-1db8-4e01-9302-0beebcb67864"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2f86287a-4c97-48eb-9d91-8c18dda57bf2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("72f380ce-a322-47b9-a8de-3c00e2fd834f"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7fc8cc8c-78fa-493f-909c-2b02ca0f848a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ed7e1efb-988d-4008-8cce-22c564eed336"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Created", "CreatedBy", "Detail", "LastModified", "LastModifiedBy", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { new Guid("0996cf33-6b9b-407d-be5e-335a992661a5"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 12", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 12", 29.989999999999998, 5 },
                    { new Guid("0e4d0def-55ef-4673-be2b-c1aa858b3a0f"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 11", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 11", 19.989999999999998, 10 },
                    { new Guid("31eb3c59-d7db-4465-8c6b-1c59c54acdf2"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 13", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 13", 15.5, 20 },
                    { new Guid("6fd4cd16-4bf4-43f0-bfbd-270421271eaa"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 14", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 14", 45.0, 7 },
                    { new Guid("ea0bc2f4-269c-4779-bb8d-d55bfea6ee8b"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Detail of Product 15", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Product 15", 9.9900000000000002, 30 }
                });
        }
    }
}
