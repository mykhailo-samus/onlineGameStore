using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.AutoMapper;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.DAL.Entities;
using AutoMapper;
using Moq;

namespace OnlineGameStore.Tests
{
    [TestClass]
    public class GamesServiceTest
    {
        private GameService gameService;
        private GameDTO gameDTO;
        private List<Game> games;
        private Mock<IUnitOfWork> mockUow;
        string keyName;
        [TestInitialize]
        public void Initialize()
        {
            //arrange
            AutoMapperBLLConfiguration.Configure();
            mockUow = new Mock<IUnitOfWork>();
            gameService = new GameService(mockUow.Object);
            gameDTO = new GameDTO { GameKey = Guid.NewGuid().ToString(), Name = "Diablo", Description = "Great game" };
            games = new List<Game> 
            { 
              new Game {GameKey = keyName + "1", Description = "description1", Name = "game1"}, 
              new Game {GameKey = keyName + "1", Description = "description2", Name = "game2"}
            };
            keyName = "key";
        }

       [TestMethod]
        public void CreateGame_ThroughGameRepository_IsCalled()
        {
           //arrange
           mockUow.Setup(x => x.GameRepository.Create(Mapper.Map<Game>(gameDTO))).Verifiable();
           //act
           gameService.Create(gameDTO);
           //assert
           mockUow.Verify(x => x.GameRepository.Create(It.IsAny<Game>()), Times.Once);
        }

       [TestMethod]
       public void UpdateGame_ThroughGameRepository_IsCalled()
       {
           //arrange
           mockUow.Setup(x => x.GameRepository.Update(Mapper.Map<Game>(gameDTO))).Verifiable();
           //act
           gameService.Update(gameDTO);
           //assert
           mockUow.Verify(x => x.GameRepository.Update(It.IsAny<Game>()), Times.Once);
       }

       [TestMethod]
       public void DeleteGame_ThroughGameRepository_IsCalled()
       {
           //arrange
           mockUow.Setup(x => x.GameRepository.Remove(Mapper.Map<Game>(gameDTO))).Verifiable();
           //act
           gameService.Remove(gameDTO);
           //assert
           mockUow.Verify(x => x.GameRepository.Remove(It.IsAny<Game>()), Times.Once);
       }

       [TestMethod]
       public void DetachGame_ThroughGameRepository_IsCalled()
       {
           //arrange
           mockUow.Setup(x => x.GameRepository.Detach(Mapper.Map<Game>(gameDTO))).Verifiable();
           //act
           gameService.Detach(gameDTO);
           //assert
           mockUow.Verify(x => x.GameRepository.Detach(It.IsAny<Game>()), Times.Once);
       }

       [TestMethod]
       public void GetGamesByGenre_ThroughGameRepository_ReturnsGameDtoList()
       {
           //arrange
           mockUow.Setup(x => x.GameRepository.GetByGenre(keyName)).Returns(games);
           //act
           var actualGames = gameService.GetByGenre(keyName);
           //assert
           mockUow.Verify(x => x.GameRepository.GetByGenre(keyName), Times.Once);
           Assert.IsInstanceOfType(actualGames, typeof(List<GameDTO>));

       }

       [TestMethod]
       public void GetGameByKey_ThroughGameRepository_ReturnsGameDto()
       {
           //arrange
           var game = Mapper.Map<Game>(gameDTO);
           mockUow.Setup(x => x.GameRepository.GetByKey(keyName)).Returns(game);
           //act
           var actualGame = gameService.GetByKey(keyName);
           //assert
           mockUow.Verify(x => x.GameRepository.GetByKey(keyName), Times.Once);
           Assert.IsInstanceOfType(actualGame, typeof(GameDTO));
       }

       [TestMethod]
       public void GetGamesByPlatform_ThroughGameRepository_ReturnsGameDtoList()
       {
           //arrange
           mockUow.Setup(x => x.GameRepository.GetByPlatform(keyName)).Returns(games);
           //act
           var actualGames = gameService.GetByPlatform(keyName);
           //assert
           mockUow.Verify(x => x.GameRepository.GetByPlatform(keyName), Times.Once);
           Assert.IsInstanceOfType(actualGames, typeof(List<GameDTO>));
       }

       [TestMethod]
       public void GetAllGames_ThroughGameRepository_ReturnsGameDtoList()
       {
           mockUow.Setup(x => x.GameRepository.GetAll()).Returns(games);
           //act
           var actualGames = gameService.GetAll();
           //assert
           mockUow.Verify(x => x.GameRepository.GetAll(), Times.Once);
           Assert.IsInstanceOfType(actualGames, typeof(List<GameDTO>));
       }
    }
}
