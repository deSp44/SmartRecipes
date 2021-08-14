using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartRecipesMVC.Domain.Model;
using SmartRecipesMVC.Domain.Model.Connections;

namespace SmartRecipesMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<RecipeTag> RecipeTag { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // RECIPE <-> RECIPE-TAG <-> TAG
            builder.Entity<RecipeTag>()
                .HasKey(it => new {it.RecipeId, it.TagId});
            builder.Entity<RecipeTag>()
                .HasOne<Recipe>(it => it.Recipe)
                .WithMany(i => i.RecipeTags)
                .HasForeignKey(it => it.RecipeId);
            builder.Entity<RecipeTag>()
                .HasOne<Tag>(it => it.Tag)
                .WithMany(i => i.RecipeTags)
                .HasForeignKey(it => it.TagId);

            // RECIPE <-> RECIPE-INGREDIENT <-> INGREDIENT
            builder.Entity<RecipeIngredient>()
                .HasKey(it => new { it.RecipeId, it.IngredientId });
            builder.Entity<RecipeIngredient>()
                .HasOne<Recipe>(it => it.Recipe)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(it => it.RecipeId);
            builder.Entity<RecipeIngredient>()
                .HasOne<Ingredient>(it => it.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(it => it.IngredientId);

            // DIFFICULTIES <-ONE-TO-MANY-> RECIPE
            builder.Entity<Recipe>()
                .HasOne(it => it.Difficulty)
                .WithMany(i => i.Recipes);

            // RECIPE <-ONE-TO-MANY-> IMAGE
            builder.Entity<Image>()
                .HasOne(it => it.Recipe)
                .WithMany(i => i.Images);
        }
    }
}
