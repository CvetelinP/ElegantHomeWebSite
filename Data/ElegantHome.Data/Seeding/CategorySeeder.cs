using System;
using System.Linq;
using System.Threading.Tasks;
using ElegantHome.Data.Common.Repositories;
using ElegantHome.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElegantHome.Data.Seeding
{
    public class CategorySeeder:ISeeder
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category { Name = "Порцелан" });
            await dbContext.Categories.AddAsync(new Category { Name = "Дърво" });
            await dbContext.Categories.AddAsync(new Category { Name = "Стъкло" });
            await dbContext.Categories.AddAsync(new Category { Name = "Ратан" });
            await dbContext.Categories.AddAsync(new Category { Name = "Градина" });
            await dbContext.Categories.AddAsync(new Category { Name = "Свещи" });
            await dbContext.Categories.AddAsync(new Category { Name = "Осветление лампи" });
            await dbContext.Categories.AddAsync(new Category { Name = "Икони" });
            await dbContext.Categories.AddAsync(new Category { Name = "Цветя" });
            await dbContext.Categories.AddAsync(new Category { Name = "Коледа" });
            await dbContext.Categories.AddAsync(new Category { Name = "Намаления" });
            await dbContext.Categories.AddAsync(new Category { Name = "Часовници" });
            await dbContext.Categories.AddAsync(new Category { Name = "Паравани" });

            await dbContext.SaveChangesAsync();
        }
    }
}
