using KonusarakOgrenApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KonusarakOgrenApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<QuestionViewModel> QuestionViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Password = "password" },
                new User { Id = 2, Username = "user", Password = "password" }
                );
            modelBuilder.Entity<Options>().HasData(
                new Options { Id = 1, Text = "A" },
                new Options { Id = 2, Text = "B" },
                new Options { Id = 3, Text = "C" },
                new Options { Id = 4, Text = "D" }
                );
            

        }
    }
}
