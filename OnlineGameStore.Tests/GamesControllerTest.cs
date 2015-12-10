﻿using System;
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
    public class GamesControllerTest
    {
        private GamesController gamesController;
        private Mock<IGameService> mockGameService;
        [TestInitialize]
        public void Initialize()
        {
            //arrange
            AutoMapperWebConfiguration.Configure();
            mockGameService = new Mock<IGameService>();
            gamesController = new GamesController(mockGameService.Object);
        }

        [TestMethod]
        public void GetGames_ForValidData_CallsGetlAllOnce()
        {
            //arranged in initializer
            //act
            gamesController.GetGames();
            //assert
            mockGameService.Verify(x => x.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetGame_ForNonexistentKey_ReturnsNotFoundResult()
        {
            //arrange
            var key = "gameKey";
            mockGameService.Setup(x => x.GetByKey(key)).Returns((GameDTO)null);

            //act
            var result = gamesController.GetGame(key);

            //assert
            mockGameService.Verify(x => x.GetByKey(key), Times.Once);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetGame_ForExistingKey_ReturnsOkNegConResult()
        {
            //arrange
            var key = "gameKey";
            var game = new GameDTO { GameKey = key, Name = "Game" };
            mockGameService.Setup(x => x.GetByKey(key)).Returns(game);

            //act
            var result = gamesController.GetGame(key);

            //assert
            mockGameService.Verify(x => x.GetByKey(key), Times.Once);
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GameVM>));
        }

        [TestMethod]
        public void UpdateGame_ForExistingGame_ReturnsCreatedAtRouteResult()
        {
            //arrange
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };

            mockGameService.Setup(x => x.GetByKey(key)).Returns(Mapper.Map<GameDTO>(game));
            //act
            var result = gamesController.Update(game);

            //assert
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<GameVM>));
        }

        [TestMethod]
        public void CreateGame_ForExistingGame_ReturnsCreatedAtRouteResult()
        {
            // Arrange
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };
            mockGameService.Setup(x => x.GetByKey(key)).Returns(Mapper.Map<GameDTO>(game));

            // Act
            var result = gamesController.New(game);

            // Assert
            mockGameService.Verify(m => m.Create(It.IsAny<GameDTO>()), Times.Once);
            Assert.IsInstanceOfType(result, typeof(CreatedAtRouteNegotiatedContentResult<GameVM>));
        }

        [TestMethod]
        public void DeleteGame_ForExistentGame_ReturnsOkResult()
        {
            // Arrange
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };
            mockGameService.Setup(x => x.GetByKey(key)).Returns(Mapper.Map<GameDTO>(game));

            // Act
            var result = gamesController.Delete(game);

            // Assert
            mockGameService.Verify(m => m.Remove(It.IsAny<GameDTO>()), Times.Once);
            Assert.IsInstanceOfType(result,  typeof(OkResult));
        }

        [TestMethod]
        public void CreateGame_ForInvalidModel_ReturnsInvalidModelResult()
        {
            // Arrange
            gamesController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };

            // Act
            var result = gamesController.New(game);

            // Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void UpdateGame_ForNonexistentGame_ReturnsNotFoundResult()
        {
            // Arrange
            gamesController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };

            // Act
            var result = gamesController.Update(game);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateGame_ForInvalidModel_ReturnsInvalidModelResult()
        {
            // Arrange
            gamesController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };
            mockGameService.Setup(x => x.GetByKey(key)).Returns(Mapper.Map<GameDTO>(game));

            // Act
            var result = gamesController.Update(game);

            // Assert
            Assert.IsInstanceOfType(result, typeof(InvalidModelStateResult));
        }
        [TestMethod]
        public void DeleteGame_ForNonexistentGame_ReturnsNotFoundResult()
        {
            // Arrange
            gamesController.ModelState.AddModelError("InvalidModel", "Invalid Model");
            var key = "gameKey";
            var game = new GameVM { GameKey = key, Name = "Game" };

            // Act
            var result = gamesController.Delete(game);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

    }
}
