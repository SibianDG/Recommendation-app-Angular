using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappers
{
    internal class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            //builder.ToTable("Item");
            builder.HasKey(t => t.ItemId);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Summary).IsRequired();

            builder.HasDiscriminator<string>("TypeName")
                .HasValue<Book>("Book")
                .HasValue<Movie>("Movie")
                .HasValue<Serie>("Serie");
        }
    }
}