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
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.GameKey = key;
            comment.ParentId = null;
            commentService.Create(comment);
            return CreatedAtRoute("comments", new { key = comment.GameKey }, comment);
        }

        [HttpPost]
        [Route("comments/{id}/newcomment")]
        public IHttpActionResult AddCommentForComment(int id, CommentVM commentVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentComment = commentService.GetCommentById(id);
            if (parentComment == null)
            {
                return NotFound();
            }
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.ParentId = id;

            comment.GameKey = parentComment.GameKey;
            commentService.Create(comment);
            return CreatedAtRoute("comments", new { key = comment.GameKey }, comment);
        }

        
        // GET games/{key}/comments
        [HttpGet]
        [Route("games/{key}/comments", Name="comments")]
        public IEnumerable<CommentVM> GetCommentsByGameKey(string key)
        {
            return Mapper.Map<IEnumerable<CommentVM>>(commentService.GetCommentsByGameKey(key));
        }
    }
}
