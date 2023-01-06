using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transactions.AccessData.Migrations
{
    public partial class trxdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovementHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    OperationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromAccountId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FromCbu = table.Column<string>(type: "nvarchar(22)", nullable: false),
                    FullNameEmisorCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DniEmisorCustomer = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CuilEmisorCustomer = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    ToAccountId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ToCbu = table.Column<string>(type: "nvarchar(22)", nullable: false),
                    FullNameReceiverCustomer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DniReceiverCustomer = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    CuilReceiverCustomer = table.Column<string>(type: "nvarchar(13)", nullable: false),
                    DateTimeTransaction = table.Column<DateTime>(type: "DateTime", nullable: false),
                    AmountTransaction = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", nullable: false),
                    ResultingStateOfTransaction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovementHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    FromAccountId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    ToAccountId = table.Column<Guid>(type: "UniqueIdentifier", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    OperationTypeId = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(15,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "DateTime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_OperationType_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionState_State",
                        column: x => x.State,
                        principalTable: "TransactionState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MovementHistory",
                columns: new[] { "Id", "AmountTransaction", "CuilEmisorCustomer", "CuilReceiverCustomer", "Currency", "DateTimeTransaction", "DniEmisorCustomer", "DniReceiverCustomer", "FromAccountId", "FromCbu", "FullNameEmisorCustomer", "FullNameReceiverCustomer", "OperationType", "ResultingStateOfTransaction", "ToAccountId", "ToCbu" },
                values: new object[] { new Guid("2cc7cc14-3f27-441f-9d6f-f343dc0beb18"), 10000.5m, "12-33123123-8", "12-22123123-8", "ARS", "2022-10-20", "33.123.123", "22.123.123", new Guid("7a9be41e-a0ce-4a2c-bb97-2a51330ee1eb"), "123456789", "Carlos Franco", "Luciano Franco", "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR", "TRX SUCCESS", new Guid("8e316f1a-85d3-462c-bb3d-98ef3456eb62"), "987654321" });

            migrationBuilder.InsertData(
                table: "OperationType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TRANSFERENCIA ENTRE CUENTAS DE MISMO TITULAR" },
                    { 2, "TRANSFERENCIA ENTRE CUENTAS DE DIFERENTE TITULAR" },
                    { 3, "INGRESO DE DINERO POR VENTANILLA" },
                    { 4, "EXTRACCION DE DINERO POR VENTANILLA" }
                });

            migrationBuilder.InsertData(
                table: "TransactionState",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "TRX SUCCESS" },
                    { 2, "TRX REJECTED" },
                    { 3, "TRX PENDING" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OperationTypeId",
                table: "Transaction",
                column: "OperationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_State",
                table: "Transaction",
                column: "State");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovementHistory");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "OperationType");

            migrationBuilder.DropTable(
                name: "TransactionState");
        }
    }
}
