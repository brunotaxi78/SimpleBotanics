using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClassLib.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValorBonus = table.Column<double>(type: "float", nullable: false),
                    NumBonus = table.Column<int>(type: "int", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<double>(type: "float", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillingAddresses",
                columns: table => new
                {
                    BillingAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAddresses", x => x.BillingAddressId);
                    table.ForeignKey(
                        name: "FK_BillingAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    CreditCardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    SecurityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipAddresses",
                columns: table => new
                {
                    ShipAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipAddresses", x => x.ShipAddressId);
                    table.ForeignKey(
                        name: "FK_ShipAddresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSuppliers",
                columns: table => new
                {
                    ProductSupplierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SupplierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSuppliers", x => x.ProductSupplierID);
                    table.ForeignKey(
                        name: "FK_ProductsSuppliers_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsSuppliers_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Orderdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Freight = table.Column<double>(type: "float", nullable: false),
                    SalesTax = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Paid = table.Column<double>(type: "float", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    BillingAddressId = table.Column<int>(type: "int", nullable: true),
                    ShipAddressId = table.Column<int>(type: "int", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_BillingAddresses_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "BillingAddresses",
                        principalColumn: "BillingAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_ShipAddresses_ShipAddressId",
                        column: x => x.ShipAddressId,
                        principalTable: "ShipAddresses",
                        principalColumn: "ShipAddressId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Statuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Tax = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description", "SubCategory" },
                values: new object[,]
                {
                    { 1, "Plantas", null, 0 },
                    { 2, "Acessórios", null, 0 },
                    { 3, "Vasos", null, 0 },
                    { 4, "Suportes", null, 0 },
                    { 5, "Sementes", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Iniciado" },
                    { 2, "Em pagamento" },
                    { 3, "Pago" },
                    { 4, "A processar" },
                    { 5, "Enviada" },
                    { 6, "Terminada" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Color", "Description", "Name", "Picture", "Price", "Ranking", "SKU", "Size", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "", "A BORRACHINHA é nativa da Índia e do sul da Ásia. Conhecêmo-la em tamanho (muito) grande de muitos parques e jardins, mas é uma planta que é igualmente feliz dentro de casa. E nós com ela!", "Borrachinha", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Borrachinha.jpg", 9m, 1.0, 111111111, 0.0, 5.0 },
                    { 2, 1, "", "Na natureza a PAXA é feliz nas florestas tropicais da América Central e do Sul. A suas folhas a cair da horizontal e o seu tronco entrançado fazem dela uma planta especial. Reserva-lhe um lugar de destaque. Ela merece.", "Paxa", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Paxa.jpg", 21m, 1.0, 222222222, 0.0, 10.0 },
                    { 3, 2, "", "Para o momento de pausa em que regamos as nossas plantas, por que não fazê-lo com estilo? Este regador da Haws, feito à mão, vai ficar lindo em qualquer estante da tua casa!", "Regador Metal", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/regador.png", 39m, 1.0, 333333333, 0.0, 10.0 },
                    { 4, 2, "", "Ideal para dares às tuas plantas uma dose extra de humidade tropical, quer seja no Verão quando os dias estão quentes e secos, quer seja no Inverno quando os aquecimentos estão ligados e a humidade relativa do ar baixa. O nosso prato tropicalizador de ambiente é o complemento ideal para qualquer “plant parent” que adora ter a sua selva em casa sempre bonita.", "Tropicalizador de ambiente", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/tropicalizador.jpg", 20m, 1.0, 444444444, 0.0, 1.0 },
                    { 5, 3, "", "Da parceria com a AMU eco life nasceram estes vasos em cortiça. Um artigo decorativo ideal para colocares as tuas plantas e acrescentar aquele toque especial ao canto lá de casa", "Vaso cortiça redondo CORK", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_cortica.jpg", 24m, 1.0, 555555555, 0.0, 0.0 },
                    { 6, 3, "", "Da marca SIGILO CERAMICS, este conjunto de vaso + prato é uma verdadeira obra de arte. Em grés vitrificado no interior, é feito em roda de oleiro em Portugal. Disponível em três cores: cru, rosa e cinza. Combina-o com as tuas belezas verdes lá de casa.", "Vaso SIGILO CERAMICS", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_ceramics.jpg", 35m, 1.0, 666666666, 0.0, 10.0 },
                    { 7, 4, "", "Pra dar aquele destaque à sua planta preferida.Produzido em madeira Tauari o suporte de vasos de chão é criação da Soul Oficina para a Botânica e Tal.Super versátil, a peça foi desenhada para suportar todos os modelos de vasos, inclusive nosso grandão OSWALD.", "Suporte CHÃO", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_chao.jpeg", 30m, 1.0, 777777777, 0.0, 3.0 },
                    { 8, 4, "", "Os suportes de plantas ANITA são produzidos artesanalmente com ferro 8mm, o que garante resistência e estabilidade à peça. Finalizados em pintura chumbo 2 em 1 (anti-ferrugem e acabamento).", "Suporte ANITA", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_anita.jpg", 35m, 1.0, 888888888, 0.0, 0.0 },
                    { 9, 5, "", "Uma mistura F1 multicolorida de tipos Nantes. Semear ao ar livre assim que não houver mais nenhum perigo de geadas, em linhas com uma distância de 25 cm. Semeie em local ensolarado com solo bem drenado e solte bem o solo a uma profundidade de 25 a 30 cm. Após 2-3 semanas, desbaste as plantas a 5 cm. Colheita já 95 dias após a sementeira.", "Cenoura Mistura", "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/sem_cenoura.png", 2m, 1.0, 999999999, 0.0, 16.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingAddresses_CustomerId",
                table: "BillingAddresses",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_CustomerId",
                table: "CreditCards",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressId",
                table: "Orders",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShipAddressId",
                table: "Orders",
                column: "ShipAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSuppliers_ProductID",
                table: "ProductsSuppliers",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsSuppliers_SupplierID",
                table: "ProductsSuppliers",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ShipAddresses_CustomerId",
                table: "ShipAddresses",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductsSuppliers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "BillingAddresses");

            migrationBuilder.DropTable(
                name: "ShipAddresses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
