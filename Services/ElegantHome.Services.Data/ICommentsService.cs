namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Comment;

    public interface ICommentsService
    {
        Task<IEnumerable<ViewServiceModel>> GetAllByAdIdAsync(int id);

        Task<ViewServiceModel> PostAsync(CreateServiceModel comment);
    }
}
