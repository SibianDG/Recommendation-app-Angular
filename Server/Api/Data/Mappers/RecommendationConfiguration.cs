using System;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mappers
{
    public class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            //builder.ToTable("Recommendation");
            builder.HasKey(t => t.RecommendationId);
            builder
                .HasMany(t => t.Items)
                .WithMany(t => t.Recommendations);
            /*builder
                .HasMany(t => t.Items)
                .WithMany()
                .Map(t =>
                {
                    t.ToTable("ItemsRecommendations");
                    t.MapLeftKey("ItemID");
                    t.MapRightKey("RecommendationId");
                });*/
            
            //https://entityframeworkcore.com/knowledge-base/61015049/how-to-add-list-of-ingredients-to-recipe-asp-net-core-mvc
            builder.Property(e => e.Keywords)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

        }
    }
}