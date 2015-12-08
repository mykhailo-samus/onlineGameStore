using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.DAL.Entities;
namespace OnlineGameStore.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new GameStoreContext())
            {
                var x = context.Games.Add(new Game() { GameKey = "Game1", Description = "asfasfafasf" });
            }
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
