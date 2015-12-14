using System.Net.Http;
using System.Web.Http;
using OnlineGameStore.Web.Controllers;
using OnlineGameStore.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGameStore.Tests.Helpers;
using OnlineGameStore.Web.ViewModel;

namespace OnlineGameStore.Tests
{
    [TestClass]
    public class GamesRouteTests
    {
        HttpConfiguration config;

        public GamesRouteTests()
        {
            config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            WebApiConfig.Register(config);
            config.EnsureInitialized();
        }

        [TestMethod]
        public void GetGames_RequestUrl_IsValidRoute()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/games");

            var routeTester = new RouteTester(config, request);

            //check controller type
            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            //check action name
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGames()), routeTester.GetActionName());
        }

        [TestMethod]
        public void GetGame_RequestUrl_IsValidRoute()
        {
            var id = "gameKey";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/games/" + id);

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.GetGame(id)), routeTester.GetActionName());
        }

        [TestMethod]
        public void UpdateGame_RequestUrl_IsValidRoute()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/games/update");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.Update(new GameVM())), routeTester.GetActionName());
        }

        [TestMethod]
        public void NewGame_RequestUrl_IsValidRoute()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/games/new");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.New(new GameVM())), routeTester.GetActionName());
        }

        [TestMethod]
        public void RemoveGame_RequestUrl_IsValidRoute()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/games/remove");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.Delete(new GameVM())), routeTester.GetActionName());
        }

        [TestMethod]
        public void DownloadGame_RequestUrl_IsValidRoute()
        {
            var gameKey = "gameKey";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/games/" + gameKey + "/download");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(GamesController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((GamesController p) => p.Download(gameKey)), routeTester.GetActionName());
        }
    }
}
