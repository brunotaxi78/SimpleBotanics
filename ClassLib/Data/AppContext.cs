using ClassLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLib.Data
{
    public class AppContext : DbContext
    {
        public DbSet<BillingAddress> BillingAddresses { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSupplier> ProductsSuppliers { get; set; }

        public DbSet<ShipAddress> ShipAddresses { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=ProjetoRumosDB; Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                    new Status() { StatusId = 1, Name = "Iniciado" },
                    new Status() { StatusId = 2, Name = "Em pagamento" },
                    new Status() { StatusId = 3, Name = "Pago" },
                    new Status() { StatusId = 4, Name = "A processar" },
                    new Status() { StatusId = 5, Name = "Enviada" },
                    new Status() { StatusId = 6, Name = "Terminada" }
                );

            modelBuilder.Entity<Category>().HasData(
                    new Category() { CategoryId = 1, SubCategory = 0, CategoryName = "Plantas" },
                    new Category() { CategoryId = 2, SubCategory = 0, CategoryName = "Acessórios" },
                    new Category() { CategoryId = 3, SubCategory = 0, CategoryName = "Vasos" },
                    new Category() { CategoryId = 4, SubCategory = 0, CategoryName = "Suportes" },
                    new Category() { CategoryId = 5, SubCategory = 0, CategoryName = "Sementes" }
                );

            modelBuilder.Entity<Product>().HasData(
                   new Product() { ProductId = 1, 
                                   Name = "Borrachinha", 
                                   SKU = 111111111, 
                                   Description = "A BORRACHINHA é nativa da Índia e do sul da Ásia. Conhecêmo-la em tamanho (muito) grande de muitos parques e jardins, mas é uma planta que é igualmente feliz dentro de casa. E nós com ela!", 
                                   Price = 9, 
                                   Size = 0, 
                                   Stock = 5, 
                                   Color = "", 
                                   Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Borrachinha.jpg", 
                                   Ranking = 1, 
                                   CategoryId = 1
                                 },
                   new Product()
                   {
                       ProductId = 2,
                       Name = "Paxa",
                       SKU = 222222222,
                       Description = "Na natureza a PAXA é feliz nas florestas tropicais da América Central e do Sul. A suas folhas a cair da horizontal e o seu tronco entrançado fazem dela uma planta especial. Reserva-lhe um lugar de destaque. Ela merece.",
                       Price = 21,
                       Size = 0,
                       Stock = 10,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/Paxa.jpg",
                       Ranking = 1,
                       CategoryId = 1
                   },
                   new Product()
                   {
                       ProductId = 3,
                       Name = "Regador Metal",
                       SKU = 333333333,
                       Description = "Para o momento de pausa em que regamos as nossas plantas, por que não fazê-lo com estilo? Este regador da Haws, feito à mão, vai ficar lindo em qualquer estante da tua casa!",
                       Price = 39,
                       Size = 0,
                       Stock = 10,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/regador.png",
                       Ranking = 1,
                       CategoryId = 2
                   },
                   new Product()
                   {
                       ProductId = 4,
                       Name = "Tropicalizador de ambiente",
                       SKU = 444444444,
                       Description = "Ideal para dares às tuas plantas uma dose extra de humidade tropical, quer seja no Verão quando os dias estão quentes e secos, quer seja no Inverno quando os aquecimentos estão ligados e a humidade relativa do ar baixa. O nosso prato tropicalizador de ambiente é o complemento ideal para qualquer “plant parent” que adora ter a sua selva em casa sempre bonita.",
                       Price = 20,
                       Size = 0,
                       Stock = 1,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/tropicalizador.jpg",
                       Ranking = 1,
                       CategoryId = 2
                   }, 
                   new Product()
                   {
                       ProductId = 5,
                       Name = "Vaso cortiça redondo CORK",
                       SKU = 555555555,
                       Description = "Da parceria com a AMU eco life nasceram estes vasos em cortiça. Um artigo decorativo ideal para colocares as tuas plantas e acrescentar aquele toque especial ao canto lá de casa",
                       Price = 24,
                       Size = 0,
                       Stock = 0,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_cortica.jpg",
                       Ranking = 1,
                       CategoryId = 3
                   },
                   new Product()
                   {
                       ProductId = 6,
                       Name = "Vaso SIGILO CERAMICS",
                       SKU = 666666666,
                       Description = "Da marca SIGILO CERAMICS, este conjunto de vaso + prato é uma verdadeira obra de arte. Em grés vitrificado no interior, é feito em roda de oleiro em Portugal. Disponível em três cores: cru, rosa e cinza. Combina-o com as tuas belezas verdes lá de casa.",
                       Price = 35,
                       Size = 0,
                       Stock = 10,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/vaso_ceramics.jpg",
                       Ranking = 1,
                       CategoryId = 3
                   },
                   new Product()
                   {
                       ProductId = 7,
                       Name = "Suporte CHÃO",
                       SKU = 777777777,
                       Description = "Pra dar aquele destaque à sua planta preferida.Produzido em madeira Tauari o suporte de vasos de chão é criação da Soul Oficina para a Botânica e Tal.Super versátil, a peça foi desenhada para suportar todos os modelos de vasos, inclusive nosso grandão OSWALD.",
                       Price = 30,
                       Size = 0,
                       Stock = 3,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_chao.jpeg",
                       Ranking = 1,
                       CategoryId = 4
                   },
                   new Product()
                   {
                       ProductId = 8,
                       Name = "Suporte ANITA",
                       SKU = 888888888,
                       Description = "Os suportes de plantas ANITA são produzidos artesanalmente com ferro 8mm, o que garante resistência e estabilidade à peça. Finalizados em pintura chumbo 2 em 1 (anti-ferrugem e acabamento).",
                       Price = 35,
                       Size = 0,
                       Stock = 0,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/suporte_anita.jpg",
                       Ranking = 1,
                       CategoryId = 4
                   },
                   new Product()
                   {
                       ProductId = 9,
                       Name = "Cenoura Mistura",
                       SKU = 999999999,
                       Description = "Uma mistura F1 multicolorida de tipos Nantes. Semear ao ar livre assim que não houver mais nenhum perigo de geadas, em linhas com uma distância de 25 cm. Semeie em local ensolarado com solo bem drenado e solte bem o solo a uma profundidade de 25 a 30 cm. Após 2-3 semanas, desbaste as plantas a 5 cm. Colheita já 95 dias após a sementeira.",
                       Price = 2,
                       Size = 0,
                       Stock = 16,
                       Color = "",
                       Picture = "https://asprojeto.blob.core.windows.net/simpleotanicsimages/Podutos/sem_cenoura.png",
                       Ranking = 1,
                       CategoryId = 5
                   }
               );


        }
    }
}
