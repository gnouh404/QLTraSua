using Microsoft.AspNetCore.Mvc;
using QLTraSua.Models;

namespace QLTraSua.ViewComponents
{
    public class RenderViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent()
        {
            MenuItems = new List<MenuItem>()
            {
               new MenuItem() { Id = 1, Name = "Home", Link = "/TestAdmin/" },
                new MenuItem() { Id = 2, Name = "Order review ", Link = "/TestAdmin/Duyet/" }
            };

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
