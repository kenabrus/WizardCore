using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Entities;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Topics.Any())
            {
                Console.WriteLine($"    -   DB has been seeded"); 
                return; 
            }

            var topics = new Topic[]
            {
                Topic.Create("Frontend"),
                Topic.Create("Backend"),
                Topic.Create("Frontend and Backend")
            };
            foreach (Topic t in topics)
            {
                context.Topics.Add(t);
            }
            context.SaveChanges();
            Console.WriteLine($"    -   seeding DB ..."); 
        }
    }
}