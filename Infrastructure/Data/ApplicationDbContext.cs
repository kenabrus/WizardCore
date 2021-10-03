using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data.Config;

namespace Infrastructure.Data
{
public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages {get; set;}
        public DbSet<Topic> Topics {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageTableConfiguration());
            builder.ApplyConfiguration(new TopicTableConfiguration());
            base.OnModelCreating(builder);
        }
    }
}