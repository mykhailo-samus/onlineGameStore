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
using WebApi.OutputCache.V2;
using Serilog;

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
            Log.Debug("User requested POST: games/{key}/newcomment", key);
            if (!ModelState.IsValid)
            {
                Log.Error("Request is invalid!");
                return BadRequest(ModelState);
            }
            var game = gameService.GetByKey(key);
            if (game == null)
            {
                Log.Error("Game with certain key is not found!");
                return NotFound();
            }
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.GameKey = key;
            comment.ParentId = null;
            commentService.Create(comment);
            return CreatedAtRoute("comments", new { key = comment.GameKey }, Mapper.Map<CommentVM>(comment));
        }

        [HttpPost]
        [Route("comments/{id}/newcomment")]
        public IHttpActionResult AddCommentForComment(int id, CommentVM commentVM)
        {
            Log.Debug("User requested POST: comments/{id}/newcomment", id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentComment = commentService.GetCommentById(id);
            if (parentComment == null)
            {
                Log.Error("Parent comment with certain id is not found!");
                return NotFound();
            }
            var comment = Mapper.Map<CommentDTO>(commentVM);
            comment.ParentId = id;

            comment.GameKey = parentComment.GameKey;
            commentService.Create(comment);
            return CreatedAtRoute("comments", new { key = comment.GameKey }, Mapper.Map<CommentVM>(comment));
        }

        
        // GET games/{key}/comments
        [HttpGet]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [Route("games/{key}/comments", Name="comments")]
        public IEnumerable<CommentVM> GetCommentsByGameKey(string key)
        {
            Log.Debug("User requested GET: games/{key}/comments", key);
            return Mapper.Map<IEnumerable<CommentVM>>(commentService.GetCommentsByGameKey(key));
        }
    }
}
