﻿namespace ElegantHome.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
