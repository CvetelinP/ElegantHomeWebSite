namespace ElegantHome.Data.EntityConfigurations
{
    using ElegantHome.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CategoryConfiguration: IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> category)
        {
            category.HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
