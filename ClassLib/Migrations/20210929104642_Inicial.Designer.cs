﻿// <auto-generated />
using System;
using ClassLib.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClassLib.Migrations
{
    [DbContext(typeof(Data.AppContext))]
    [Migration("20210929104642_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ClassLib.Models.BillingAddress", b =>
                {
                    b.Property<int>("BillingAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BillingAddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("BillingAddresses");
                });

            modelBuilder.Entity("ClassLib.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SubCategory")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Plantas",
                            SubCategory = 0
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Acessórios",
                            SubCategory = 0
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Vasos",
                            SubCategory = 0
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Suportes",
                            SubCategory = 0
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Sementes",
                            SubCategory = 0
                        });
                });

            modelBuilder.Entity("ClassLib.Models.CreditCard", b =>
                {
                    b.Property<int>("CreditCardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CardName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("CardNumber")
                        .HasColumnType("int");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("ExpDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CreditCardId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CreditCards");
                });

            modelBuilder.Entity("ClassLib.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumBonus")
                        .HasColumnType("int");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ValorBonus")
                        .HasColumnType("float");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ClassLib.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BillingAddressId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<double>("Discount")
                        .HasColumnType("float");

                    b.Property<double>("Freight")
                        .HasColumnType("float");

                    b.Property<string>("Info")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Orderdate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Paid")
                        .HasColumnType("float");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<double>("SalesTax")
                        .HasColumnType("float");

                    b.Property<int?>("ShipAddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShipDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ShipAddressId");

                    b.HasIndex("StatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ClassLib.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ClassLib.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("Ranking")
                        .HasColumnType("float");

                    b.Property<int>("SKU")
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<double>("Stock")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryId = 1,
                            Color = "",
                            Description = "A BORRACHINHA é nativa da Índia e do sul da Ásia. Conhecêmo-la em tamanho (muito) grande de muitos parques e jardins, mas é uma planta que é igualmente feliz dentro de casa. E nós com ela!",
                            Name = "Borrachinha",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Borrachinha.jpg",
                            Price = 9m,
                            Ranking = 1.0,
                            SKU = 111111111,
                            Size = 0.0,
                            Stock = 5.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryId = 1,
                            Color = "",
                            Description = "Na natureza a PAXA é feliz nas florestas tropicais da América Central e do Sul. A suas folhas a cair da horizontal e o seu tronco entrançado fazem dela uma planta especial. Reserva-lhe um lugar de destaque. Ela merece.",
                            Name = "Paxa",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Paxa.jpg",
                            Price = 21m,
                            Ranking = 1.0,
                            SKU = 222222222,
                            Size = 0.0,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryId = 2,
                            Color = "",
                            Description = "Para o momento de pausa em que regamos as nossas plantas, por que não fazê-lo com estilo? Este regador da Haws, feito à mão, vai ficar lindo em qualquer estante da tua casa!",
                            Name = "Regador Metal",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/regador.png",
                            Price = 39m,
                            Ranking = 1.0,
                            SKU = 333333333,
                            Size = 0.0,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryId = 2,
                            Color = "",
                            Description = "Ideal para dares às tuas plantas uma dose extra de humidade tropical, quer seja no Verão quando os dias estão quentes e secos, quer seja no Inverno quando os aquecimentos estão ligados e a humidade relativa do ar baixa. O nosso prato tropicalizador de ambiente é o complemento ideal para qualquer “plant parent” que adora ter a sua selva em casa sempre bonita.",
                            Name = "Tropicalizador de ambiente",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/tropicalizador.jpg",
                            Price = 20m,
                            Ranking = 1.0,
                            SKU = 444444444,
                            Size = 0.0,
                            Stock = 1.0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryId = 3,
                            Color = "",
                            Description = "Da parceria com a AMU eco life nasceram estes vasos em cortiça. Um artigo decorativo ideal para colocares as tuas plantas e acrescentar aquele toque especial ao canto lá de casa",
                            Name = "Vaso cortiça redondo CORK",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_cortica.jpg",
                            Price = 24m,
                            Ranking = 1.0,
                            SKU = 555555555,
                            Size = 0.0,
                            Stock = 0.0
                        },
                        new
                        {
                            ProductId = 6,
                            CategoryId = 3,
                            Color = "",
                            Description = "Da marca SIGILO CERAMICS, este conjunto de vaso + prato é uma verdadeira obra de arte. Em grés vitrificado no interior, é feito em roda de oleiro em Portugal. Disponível em três cores: cru, rosa e cinza. Combina-o com as tuas belezas verdes lá de casa.",
                            Name = "Vaso SIGILO CERAMICS",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_ceramics.jpg",
                            Price = 35m,
                            Ranking = 1.0,
                            SKU = 666666666,
                            Size = 0.0,
                            Stock = 10.0
                        },
                        new
                        {
                            ProductId = 7,
                            CategoryId = 4,
                            Color = "",
                            Description = "Pra dar aquele destaque à sua planta preferida.Produzido em madeira Tauari o suporte de vasos de chão é criação da Soul Oficina para a Botânica e Tal.Super versátil, a peça foi desenhada para suportar todos os modelos de vasos, inclusive nosso grandão OSWALD.",
                            Name = "Suporte CHÃO",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_chao.jpeg",
                            Price = 30m,
                            Ranking = 1.0,
                            SKU = 777777777,
                            Size = 0.0,
                            Stock = 3.0
                        },
                        new
                        {
                            ProductId = 8,
                            CategoryId = 4,
                            Color = "",
                            Description = "Os suportes de plantas ANITA são produzidos artesanalmente com ferro 8mm, o que garante resistência e estabilidade à peça. Finalizados em pintura chumbo 2 em 1 (anti-ferrugem e acabamento).",
                            Name = "Suporte ANITA",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_anita.jpg",
                            Price = 35m,
                            Ranking = 1.0,
                            SKU = 888888888,
                            Size = 0.0,
                            Stock = 0.0
                        },
                        new
                        {
                            ProductId = 9,
                            CategoryId = 5,
                            Color = "",
                            Description = "Uma mistura F1 multicolorida de tipos Nantes. Semear ao ar livre assim que não houver mais nenhum perigo de geadas, em linhas com uma distância de 25 cm. Semeie em local ensolarado com solo bem drenado e solte bem o solo a uma profundidade de 25 a 30 cm. Após 2-3 semanas, desbaste as plantas a 5 cm. Colheita já 95 dias após a sementeira.",
                            Name = "Cenoura Mistura",
                            Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/sem_cenoura.png",
                            Price = 2m,
                            Ranking = 1.0,
                            SKU = 999999999,
                            Size = 0.0,
                            Stock = 16.0
                        });
                });

            modelBuilder.Entity("ClassLib.Models.ProductSupplier", b =>
                {
                    b.Property<int>("ProductSupplierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("SupplierID")
                        .HasColumnType("int");

                    b.HasKey("ProductSupplierID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SupplierID");

                    b.ToTable("ProductsSuppliers");
                });

            modelBuilder.Entity("ClassLib.Models.ShipAddress", b =>
                {
                    b.Property<int>("ShipAddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ShipAddressId");

                    b.HasIndex("CustomerId");

                    b.ToTable("ShipAddresses");
                });

            modelBuilder.Entity("ClassLib.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Name = "Iniciado"
                        },
                        new
                        {
                            StatusId = 2,
                            Name = "Em pagamento"
                        },
                        new
                        {
                            StatusId = 3,
                            Name = "Pago"
                        },
                        new
                        {
                            StatusId = 4,
                            Name = "A processar"
                        },
                        new
                        {
                            StatusId = 5,
                            Name = "Enviada"
                        },
                        new
                        {
                            StatusId = 6,
                            Name = "Terminada"
                        });
                });

            modelBuilder.Entity("ClassLib.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscountType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ClassLib.Models.BillingAddress", b =>
                {
                    b.HasOne("ClassLib.Models.Customer", "Customer")
                        .WithMany("BillingAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassLib.Models.CreditCard", b =>
                {
                    b.HasOne("ClassLib.Models.Customer", "Customer")
                        .WithMany("CreditCards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassLib.Models.Order", b =>
                {
                    b.HasOne("ClassLib.Models.BillingAddress", "BillingAddress")
                        .WithMany("Orders")
                        .HasForeignKey("BillingAddressId");

                    b.HasOne("ClassLib.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ClassLib.Models.ShipAddress", "ShipAddress")
                        .WithMany("Orders")
                        .HasForeignKey("ShipAddressId");

                    b.HasOne("ClassLib.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId");

                    b.Navigation("BillingAddress");

                    b.Navigation("Customer");

                    b.Navigation("ShipAddress");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("ClassLib.Models.OrderDetail", b =>
                {
                    b.HasOne("ClassLib.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ClassLib.Models.Product", b =>
                {
                    b.HasOne("ClassLib.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ClassLib.Models.ProductSupplier", b =>
                {
                    b.HasOne("ClassLib.Models.Product", "Product")
                        .WithMany("ProductsSuppliers")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLib.Models.Supplier", "Supplier")
                        .WithMany("ProductsSuppliers")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("ClassLib.Models.ShipAddress", b =>
                {
                    b.HasOne("ClassLib.Models.Customer", "Customer")
                        .WithMany("ShipAddresses")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassLib.Models.BillingAddress", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ClassLib.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ClassLib.Models.Customer", b =>
                {
                    b.Navigation("BillingAddresses");

                    b.Navigation("CreditCards");

                    b.Navigation("Orders");

                    b.Navigation("ShipAddresses");
                });

            modelBuilder.Entity("ClassLib.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ClassLib.Models.Product", b =>
                {
                    b.Navigation("ProductsSuppliers");
                });

            modelBuilder.Entity("ClassLib.Models.ShipAddress", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ClassLib.Models.Status", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ClassLib.Models.Supplier", b =>
                {
                    b.Navigation("ProductsSuppliers");
                });
#pragma warning restore 612, 618
        }
    }
}
