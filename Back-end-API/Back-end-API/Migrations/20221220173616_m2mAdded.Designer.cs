﻿// <auto-generated />
using System;
using Back_end_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendAPI.Migrations
{
    [DbContext(typeof(RecipeAppContext))]
    [Migration("20221220173616_m2mAdded")]
    partial class m2mAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Back_end_API.Models.FavoritesModel", b =>
                {
                    b.Property<int>("favoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("favoriteId"));

                    b.Property<int>("recipeId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("favoriteId");

                    b.HasIndex("recipeId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Back_end_API.Models.FeaturedModel", b =>
                {
                    b.Property<int>("featuredId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("featuredId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("featuredId");

                    b.ToTable("FeaturedModel");
                });

            modelBuilder.Entity("Back_end_API.Models.FeaturedRecipesModel", b =>
                {
                    b.Property<int>("recipeId")
                        .HasColumnType("int");

                    b.Property<int>("featuredId")
                        .HasColumnType("int");

                    b.HasKey("recipeId", "featuredId");

                    b.HasIndex("featuredId");

                    b.ToTable("FeaturedRecipesModel");
                });

            modelBuilder.Entity("Back_end_API.Models.RecipeModel", b =>
                {
                    b.Property<int>("recipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("recipeId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingredients")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Portions")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("date");

                    b.Property<byte[]>("imageFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("prepTime")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("recipeId");

                    b.HasIndex("userId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Back_end_API.Models.UserModel", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("userId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("activateAccountToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("activateAccountTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("activeSince")
                        .HasColumnType("date");

                    b.Property<string>("adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isAdmin")
                        .HasColumnType("bit");

                    b.Property<byte[]>("passwordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("passwordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("passwordResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("passwordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Back_end_API.Models.FavoritesModel", b =>
                {
                    b.HasOne("Back_end_API.Models.RecipeModel", "Recipe")
                        .WithMany()
                        .HasForeignKey("recipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Back_end_API.Models.FeaturedRecipesModel", b =>
                {
                    b.HasOne("Back_end_API.Models.FeaturedModel", "Featured")
                        .WithMany("FeaturedRecipes")
                        .HasForeignKey("featuredId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Back_end_API.Models.RecipeModel", "Recipe")
                        .WithMany("FeaturedRecipes")
                        .HasForeignKey("recipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Featured");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Back_end_API.Models.RecipeModel", b =>
                {
                    b.HasOne("Back_end_API.Models.UserModel", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Back_end_API.Models.FeaturedModel", b =>
                {
                    b.Navigation("FeaturedRecipes");
                });

            modelBuilder.Entity("Back_end_API.Models.RecipeModel", b =>
                {
                    b.Navigation("FeaturedRecipes");
                });
#pragma warning restore 612, 618
        }
    }
}
