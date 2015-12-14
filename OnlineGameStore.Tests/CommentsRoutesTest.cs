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
    public class CommentsRouteTests
    {
        HttpConfiguration config;

        public CommentsRouteTests()
        {
            config = new HttpConfiguration
            {
                IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
            };
            WebApiConfig.Register(config);
            config.EnsureInitialized();
        }

        [TestMethod]
        public void GetCommentsForGame_Url_IsCorrect()
        {
            var key = "gameKey";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/games/{key}/comments");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.GetCommentsByGameKey(key)), routeTester.GetActionName());
        }

       [TestMethod]
        public void AddCommentForGame_Url_IsCorrect()
        {
            var key = "Gamekey";
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/games/{key}/newcomment");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.AddCommentForGame(key, new CommentVM())), routeTester.GetActionName());
        }

       [TestMethod]
        public void AddCommentForComment_Url_IsCorrect()
        {
            var id = 51;
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/comments/{id}/newcomment");

            var routeTester = new RouteTester(config, request);

            Assert.AreEqual(typeof(CommentsController), routeTester.GetControllerType());
            Assert.AreEqual(ReflectionHelpers.GetMethodName((CommentsController p) => p.AddCommentForComment(id, new CommentVM())), routeTester.GetActionName());
        }
    }
}
