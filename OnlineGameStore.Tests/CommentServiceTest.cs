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

namespace OnlineCommentStore.Tests
{
    [TestClass]
    public class CommentServiceTest
    {
        private CommentService commentService;
        private CommentDTO commentDTO;
        private Mock<IUnitOfWork> mockUow;
        [TestInitialize]
        public void Initialize()
        {
            //arrange
            AutoMapperBLLConfiguration.Configure();
            mockUow = new Mock<IUnitOfWork>();
            commentService = new CommentService(mockUow.Object);
            commentDTO = new CommentDTO { Id = 1, Name = "Hello!", Body = "Nice to meet you!"};
        }

        [TestMethod]
        public void CreateComment_ThroughCommentRepository_IsCalled()
        {
            //arrange
            mockUow.Setup(x => x.CommentRepository.Create(Mapper.Map<Comment>(commentDTO))).Verifiable();
            //act
            commentService.Create(commentDTO);
            //assert
            mockUow.Verify(x => x.CommentRepository.Create(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public void UpdateComment_ThroughCommentRepository_IsCalled()
        {
            //arrange
            mockUow.Setup(x => x.CommentRepository.Update(Mapper.Map<Comment>(commentDTO))).Verifiable();
            //act
            commentService.Update(commentDTO);
            //assert
            mockUow.Verify(x => x.CommentRepository.Update(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public void DeleteComment_ThroughCommentRepository_IsCalled()
        {
            //arrange
            mockUow.Setup(x => x.CommentRepository.Remove(Mapper.Map<Comment>(commentDTO))).Verifiable();
            //act
            commentService.Remove(commentDTO);
            //assert
            mockUow.Verify(x => x.CommentRepository.Remove(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public void GetCommentById_ThroughCommentRepository_ReturnsCommentDto()
        {
            //arrange
            int id = 1;
            var comment = Mapper.Map<Comment>(commentDTO);
            mockUow.Setup(x => x.CommentRepository.GetCommentById(id)).Returns(comment);
            //act
            var actualComment = commentService.GetCommentById(id);
            //assert
            mockUow.Verify(x => x.CommentRepository.GetCommentById(id), Times.Once);
            Assert.IsInstanceOfType(actualComment, typeof(CommentDTO));
        }

        [TestMethod]
        public void GetCommentsByGameKey_ThroughCommentRepository_ReturnsCommentDtoList()
        {
            //arrange
            string key = "gameKey";
            List<Comment> comments = new List<Comment> 
            { 
              new Comment {Id = 1, Body = "comment1", Name = "header"}, 
              new Comment {Id = 2, Body = "comment2", Name = "header"}
            };
            mockUow.Setup(x => x.CommentRepository.GetCommentsByGameKey(key)).Returns(comments);
            //act
            var actualComments = commentService.GetCommentsByGameKey(key);
            //assert
            mockUow.Verify(x => x.CommentRepository.GetCommentsByGameKey(key), Times.Once);
            Assert.IsInstanceOfType(actualComments, typeof(List<CommentDTO>));
        }
    }
}
