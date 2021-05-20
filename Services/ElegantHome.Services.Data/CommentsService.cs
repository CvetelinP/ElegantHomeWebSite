namespace ElegantHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Common;
    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsService(IDeletableEntityRepository<Comment> commentRepository, UserManager<ApplicationUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ViewServiceModel>> GetAllByAdIdAsync(int id)
        {
            var comments = this.commentRepository.All().Where(c => c.ProductId == id).OrderBy(c => c.WrittenOn);

            return await comments.Select(c => new ViewServiceModel()
            {
                Id = c.Id,
                UserId = c.UserId,
                WrittenOn = c.WrittenOn.ToString(GlobalConstants.DateTimeFormat),
                Text = c.Text,
                Username = this.userManager.FindByIdAsync(c.UserId).GetAwaiter().GetResult().UserName,
            }).ToListAsync();
        }

        public async Task<ViewServiceModel> PostAsync(CreateServiceModel comment)
        {
            var commentForDb = new Comment
            {
                Text = comment.Text,
                ProductId = comment.ProductId,
                UserId = comment.UserId,
                WrittenOn = comment.WrittenOn,
            };

            await this.commentRepository.AddAsync(commentForDb);
            await this.commentRepository.SaveChangesAsync();

            var viewModel = new ViewServiceModel
            {
                Text = commentForDb.Text,
                Id = commentForDb.Id,
                UserId = commentForDb.UserId,
                WrittenOn = commentForDb.WrittenOn.ToString(GlobalConstants.DateTimeFormat),
                Username = this.userManager.FindByIdAsync(commentForDb.UserId).GetAwaiter().GetResult().UserName,
            };

            return viewModel;
        }
    }
}
