namespace ElegantHome.Web.BindingModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateBindingModel
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int ProductId { get; set; }
    }
}
