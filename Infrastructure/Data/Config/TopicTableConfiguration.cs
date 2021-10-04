using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TopicTableConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.ToTable("Topic");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("integer").IsRequired();
            builder.Property(p => p.Name).HasColumnType("nvarchar(64)").IsRequired();
            Console.WriteLine($"    -   TopicTableConfiguration");  
        }
    }
}