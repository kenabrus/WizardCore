using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MessageTableConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").IsRequired();
            builder.Property(p => p.FirstName).HasColumnType("nvarchar(64)").IsRequired();
            builder.Property(p => p.LastName).HasColumnType("nvarchar(128)").IsRequired(); 
            builder.Property(p => p.EmailTo).HasColumnType("nvarchar(128)").IsRequired(); 
            builder.Property(p => p.EmailCc).HasColumnType("nvarchar(128)").IsRequired(); 
            builder.Property(p => p.MessageText).HasColumnType("nvarchar(256)");
            builder.Property(p => p.SendDateTime).HasColumnType("datetime");

            Console.WriteLine($"    -   MessageTableConfiguration");  
        }
    }
}