using Back_end_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end_API.Data
{
    public class RecipeAppContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; } = null!;

        public DbSet<RecipeModel> Recipes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //update klaar zetten Tools>Nuget Package manager>PackageManager console> type in console "add-migration 'wat je hebt verandert'"
            //update batabase met Tools>Nuget Package manager>PackageManager console> type in console "Update-Database"
            optionsBuilder.UseSqlServer(@"Data Source=mssqlstud.fhict.local;Initial Catalog=dbi456410_recipeapp;User ID=dbi456410_recipeapp;Password=recipeApp");
        }

        //Mapjes Data, Migration en Models is voor het beheren van de database
    }
}
