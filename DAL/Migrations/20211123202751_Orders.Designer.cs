﻿// <auto-generated />
using System;
using DAL.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211123202751_Orders")]
    partial class Orders
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DAL.Models.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("DAL.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DateCreated")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("TotalRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateCreated", "Name", "Platform", "TotalRating");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background1.jpg",
                            Count = 50,
                            DateCreated = 2018,
                            Genre = 1,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Horizon_Zero_Dawn_mpnuy7.jpg",
                            Name = "Horizon Zero Dawn",
                            Platform = 2,
                            Price = 24.989999999999998,
                            Rating = 16,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 2,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background3.jpg",
                            Count = 100,
                            DateCreated = 2016,
                            Genre = 2,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Counter_Strike_tkkgm4.jpg",
                            Name = "Counter Strike GO",
                            Platform = 1,
                            Price = 4.9900000000000002,
                            Rating = 18,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 3,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background5.jpg",
                            Count = 500,
                            DateCreated = 2017,
                            Genre = 5,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Brawl_Stars_jwhuv1.jpg",
                            Name = "Brawl Stars",
                            Platform = 3,
                            Price = 0.98999999999999999,
                            Rating = 7,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 4,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg",
                            Count = 25,
                            DateCreated = 2020,
                            Genre = 2,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Half_Life_t0lcqj.jpg",
                            Name = "Half-Life VR",
                            Platform = 5,
                            Price = 29.989999999999998,
                            Rating = 18,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 5,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background1.jpg",
                            Count = 90,
                            DateCreated = 2008,
                            Genre = 7,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Skyrim_b3rdpm.jpg",
                            Name = "TES V Skyrim",
                            Platform = 1,
                            Price = 19.989999999999998,
                            Rating = 16,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 6,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background3.jpg",
                            Count = 200,
                            DateCreated = 2015,
                            Genre = 3,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Clash_Royale_oipsjp.jpg",
                            Name = "Clash Royale",
                            Platform = 3,
                            Price = 0.98999999999999999,
                            Rating = 7,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 7,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg",
                            Count = 75,
                            DateCreated = 2017,
                            Genre = 6,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Beat_Saber_ubvkuu.jpg",
                            Name = "Beat Saber",
                            Platform = 5,
                            Price = 5.9900000000000002,
                            Rating = 3,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 8,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background5.jpg",
                            Count = 40,
                            DateCreated = 2011,
                            Genre = 7,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Terraria_uzjoxt.jpg",
                            Name = "Terraria",
                            Platform = 1,
                            Price = 2.9900000000000002,
                            Rating = 12,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 9,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg",
                            Count = 700,
                            DateCreated = 2020,
                            Genre = 1,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Genshin_Impact_x0fd6d.jpg",
                            Name = "Genshin Impact",
                            Platform = 1,
                            Price = 5.9900000000000002,
                            Rating = 7,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 10,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background2.jpg",
                            Count = 999,
                            DateCreated = 2000,
                            Genre = 4,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Snake_toiezu.jpg",
                            Name = "Snake",
                            Platform = 4,
                            Price = 0.0,
                            Rating = 18,
                            TotalRating = 0
                        },
                        new
                        {
                            Id = 11,
                            Background = "https://res.cloudinary.com/dvweto8rq/image/upload/v1637147617/WebAPI/Background/Background4.jpg",
                            Count = 120,
                            DateCreated = 2007,
                            Genre = 2,
                            Logo = "https://res.cloudinary.com/dvweto8rq/image/upload/w_150,h_100,c_fill/WebAPI/Logo/Contra_City_r3iefw.jpg",
                            Name = "Contra city",
                            Platform = 4,
                            Price = 4.9900000000000002,
                            Rating = 16,
                            TotalRating = 0
                        });
                });

            modelBuilder.Entity("DAL.Models.ProductRating", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductRating");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AddressDelivery")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.HasOne("DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.OrderItem", b =>
                {
                    b.HasOne("DAL.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DAL.Models.ProductRating", b =>
                {
                    b.HasOne("DAL.Models.Product", "Product")
                        .WithMany("Ratings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("DAL.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("DAL.Models.Product", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("DAL.Models.User", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
