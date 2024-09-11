using Bookly.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Data
{
    public class AplicationDbContext :IdentityDbContext<IdentityUser>
    {
        public AplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product> Company { get; set; }
        public DbSet<ShoppingCart> SoppingCarts { get; set; }


        public DbSet<AplicationUser> AplicationUser { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
              new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
              new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
              new Category { Id = 3, Name = "History", DisplayOrder = 3 }


              );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero",
                    ISBN = "SW9999001",
                    LastPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 1,
                    ImageURL = ""

                },
                   new Product
                   {
                       Id = 2,
                       Title = "Dark Skies",
                       Author = "Nancy Hoover",
                       Description = "Praesent vitae sodales libero",
                       ISBN = "CAW777777701",
                       LastPrice = 40,
                       Price = 30,
                       Price50 = 25,
                       Price100 = 20,
                       CategoryId = 2,
                       ImageURL = ""

                   },
                      new Product
                      {
                          Id = 3,
                          Title = "Vanish in the Sunset",
                          Author = "Julian Button",
                          Description = "Praesent vitae sodales libero",
                          ISBN = "RIT0555501",
                          LastPrice = 55,
                          Price = 50,
                          Price50 = 40,
                          Price100 = 35,
                          CategoryId = 2,
                          ImageURL = ""

                      },
                         new Product
                         {
                             Id = 4,
                             Title = "Cotton Cnady",
                             Author = "Abby Muscles",
                             Description = "Praesent vitae sodales libero",
                             ISBN = "WS3333333301",
                             LastPrice = 70,
                             Price = 65,
                             Price50 = 60,
                             Price100 = 55,
                             CategoryId = 1,
                             ImageURL = ""
                         },
                            new Product
                            {
                                Id = 5,
                                Title = "Rock in the Ocen",
                                Author = "Ron Parker",
                                Description = "Praesent vitae sodales libero",
                                ISBN = "SOTJ11111111101",
                                LastPrice = 30,
                                Price = 27,
                                Price50 = 25,
                                Price100 = 20,
                                CategoryId = 3,
                                ImageURL = ""

                            },
                               new Product
                               {
                                   Id = 6,
                                   Title = "Leaves and Wonders",
                                   Author = "Laura Phantom",
                                   Description = "Praesent vitae sodales libero",
                                   ISBN = "FOT0000001",
                                   LastPrice = 25,
                                   Price = 23,
                                   Price50 = 22,
                                   Price100 = 20,
                                   CategoryId = 3,
                                   ImageURL = ""

                               }




                );
        }

    }
}
