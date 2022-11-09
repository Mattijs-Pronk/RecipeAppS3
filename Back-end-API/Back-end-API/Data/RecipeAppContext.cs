﻿using Back_end_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Data
{
    public class RecipeAppContext : DbContext
    {
        public RecipeAppContext(DbContextOptions<RecipeAppContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; } = null!;

        public DbSet<RecipeModel> Recipes { get; set; } = null!;

        public DbSet<FavoritesModel> Favorites { get; set; } = null!;


        //update klaar zetten Tools>Nuget Package manager>PackageManager console> type in console "add-migration 'wat je hebt verandert'"
        //update batabase met Tools>Nuget Package manager>PackageManager console> type in console "Update-Database"
        //Mapjes Data, Migration en Models is voor het beheren van de database
    }
}
