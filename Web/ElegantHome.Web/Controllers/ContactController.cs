namespace ElegantHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactController : Controller
    {
        public IActionResult IndexContact()
        {
            return this.View();
        }
    }
}
