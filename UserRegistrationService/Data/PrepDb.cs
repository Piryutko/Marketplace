using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserRegistrationService.Models;

namespace UserRegistrationService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
             SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }

        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Users.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Users.AddRange(
                    new User("Vasya", "Pupkin", "VASYANPRO", new DateTime(1996, 1, 1),"pupkin@gmail.com")
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }

        }
    }
}