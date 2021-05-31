
namespace ElegantHome.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using ElegantHome.Web.BindingModels.Comments;
    using ElegantHome.Web.ViewModels.Comment;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductsService productsService;

        public CommentController(ICommentsService commentsService, UserManager<ApplicationUser> userManager, IProductsService productsService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.productsService = productsService;
        }

        public async Task<IActionResult> GetAllByAd(int id)
        {
            var result = await this.commentsService.GetAllByAdIdAsync(id);

            return this.Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateBindingModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Redirect($"/Products/Details?productId={input.ProductId}");
            }

                var serviceModel = new CreateServiceModel
                {
                    Text = input.Text,
                    ProductId = input.ProductId,
                    WrittenOn = DateTime.UtcNow,
                    UserId = this.userManager.GetUserAsync(this.HttpContext.User).GetAwaiter().GetResult().Id,
                };

                
            var comment = await this.commentsService.PostAsync(serviceModel);
            return this.Redirect($"/Products/Details?productId={input.ProductId}");
        }
    }
}
