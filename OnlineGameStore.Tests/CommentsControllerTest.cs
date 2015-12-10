using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGameStore.Web.Controllers;
using OnlineGameStore.BLL.Interfaces;
using System.Web.Http.Results;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.Web.ViewModel;
using OnlineGameStore.Web.AutoMapper;
using Moq;
using AutoMapper;

namespace OnlineGameStore.Tests
{
    [TestClass]
    public class CommentsControllerTest
    {
        private CommentsController commentsController;
        private Mock<IGameService> mockGameService;
        private Mock<ICommentService> mockCommentService;
        [TestInitialize]
        public void Initialize()
        {
            //arrange
            AutoMapperWebConfiguration.Configure();
            mockGameService = new Mock<IGameService>();
            mockCommentService = new Mock<ICommentService>();
            commentsController = new CommentsController(mockGameService.Object, mockCommentService.Object);
        }

        [TestMethod]
        public void AddCommentForGame_ExistingGame_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            string gameKey = "someKey";
            var comment = new CommentVM { Name = "Author", Body = "Some body" };
            mockGameService.Setup(m => m.GetByKey(gameKey)).Returns(new GameDTO());

            // Act
            var result = commentsController.AddCommentForGame(gameKey, comment);

            // Assert
            mockCommentService.Verify(m => m.Create(It.IsAny<CommentDTO>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<CommentVM>));
        }

        [TestMethod]
        public void AddCommentForGame_NonExistentGame_ReturnsNotFoundResult()
        {
            // Arrange
            string gameKey = "someKey";
            var comment = new CommentVM { Name = "Author", Body = "Some body" };
            mockGameService.Setup(m => m.GetByKey(gameKey)).Returns((GameDTO)null);

            // Act
            var result = commentsController.AddCommentForGame(gameKey, comment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCommentByGameKey_ForValidKey_CallsGetCommentsByGameKeyOnce()
        {
            // Arrange
            string gameKey = "someKey";

            // Act
            commentsController.GetCommentsByGameKey(gameKey);

            // Assert
            mockCommentService.Verify(m => m.GetCommentsByGameKey(gameKey), Times.Once);
        }

        [TestMethod]
        public void AddCommentForComment_ForExistingComment_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var id = 1;
            var comment = new CommentVM { Name = "Author", Body = "Some body" };
            mockCommentService.Setup(m => m.GetCommentById(id)).Returns(new CommentDTO());

            // Act
            var result = commentsController.AddCommentForComment(id, comment);

            // Assert
            mockCommentService.Verify(m => m.Create(It.IsAny<CommentDTO>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<CommentVM>));
        }

        [TestMethod]
        public void AddCommentForComment_ForNonexistentComment_ReturnsNotFoundResult()
        {
            // Arrange
            var id = 1;
            var comment = new CommentVM { Name = "Author", Body = "Some body" };
            mockCommentService.Setup(m => m.GetCommentById(id)).Returns((CommentDTO)null);

            // Act
            var result = commentsController.AddCommentForComment(id, comment);

            // Assert
            Assert.IsInstanceOfType(result,typeof(NotFoundResult));
        }

        [TestMethod]
        public void AddCommentForComment_ForNonexistentGame_ReturnsInvalidModelResult()
        {
            // Arrange
            commentsController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            var id = 1;
            var comment = new CommentVM { Name = "Author", Body = "Some body" }; 
            mockCommentService.Setup(m => m.GetCommentById(id)).Returns((CommentDTO)null);

            // Act
            var result = commentsController.AddCommentForComment(id, comment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void AddCommentForGame_ForExistingGame__ReturnsInvalidModelResult()
        {
            // Arrange
            commentsController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            string gameKey = "someKey";
            var comment = new CommentVM { Name = "Author", Body = "Some body" };

            mockGameService.Setup(m => m.GetByKey(gameKey)).Returns((GameDTO)null);

            // Act
            var result = commentsController.AddCommentForGame(gameKey, comment);

            // Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }
    }
}
