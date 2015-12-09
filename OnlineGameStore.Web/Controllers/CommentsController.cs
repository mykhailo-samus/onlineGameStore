using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.Web.ViewModel;
using AutoMapper;

namespace OnlineGameStore.Web.Controllers
{
    public class CommentsController : ApiController
    {
        private IGameService gameService;
        private ICommentService commentService;
        public CommentsController(IGameService gameService, ICommentService commentService)
        {
            this.gameService = gameService;
            this.commentService = commentService;
        }

        //POST games/{key}/newcomment
        [HttpPost]
        [Route("games/{key}/newcomment")]
        public IHttpActionResult AddCommentForGame(string key, CommentVM commentVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var game = gameService.GetByKey(key);
            if (game == null)
            {
                return NotFound();
            }
            Mapper.CreateMap<CommentVM, CommentDTO>();
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.GameKey = key;

            commentService.Create(comment);
            return CreatedAtRoute("DefaultApi", new { id = commentVM.Id }, commentVM);
        }

        [HttpPost]
        [Route("comments/{id}/newcomment")]
        public IHttpActionResult AddCommentForComment(int id, CommentVM commentVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentComment = commentService;
            if (parentComment == null)
            {
                return NotFound();
            }
            Mapper.CreateMap<CommentVM, CommentDTO>();
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.ParentId = id;

            commentService.Create(comment);
            return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
        }

        
        // GET games/{key}/comments
        [HttpGet]
        [Route("games/{key}/comments")]
        public IEnumerable<CommentVM> GetCommentsByGameKey(string key)
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            return Mapper.Map<IEnumerable<CommentVM>>(commentService.GetCommentsByGameKey(key));
        }
    }
}
