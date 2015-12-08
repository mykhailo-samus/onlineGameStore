using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.Web.ViewModel;
using AutoMapper;
using System.Web.Script.Serialization;

namespace OnlineGameStore.Web.Controllers
{
    public class GamesController : ApiController
    {

        private IGameService gameService;
        private ICommentService commentService;
        public GamesController(IGameService gameService, ICommentService commentService)
        {
            this.gameService = gameService;
            this.commentService = commentService;
        }


        // GET /Games
        [Route("games")]
        public IEnumerable<GameVM> GetGames()
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            return Mapper.Map<IEnumerable<GameVM>>(gameService.GetAll());
        }
    
         //GET api/Games/5
         [Route("games/{key}")]
        [ResponseType(typeof(GameVM))]
        public IHttpActionResult GetGame(string key)
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            var game = Mapper.Map<GameVM>(gameService.GetByKey(key));
            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

         //DELETE api/Games/5
        [Route("games/update/{key}")]
         [ResponseType(typeof(GameVM))]
         public IHttpActionResult Update(string key)
         {
             Mapper.CreateMap<GameDTO, GameVM>();
             var game = gameService.GetByKey(key);
             if (game == null)
             {
                 return NotFound();
             }
             gameService.Update(game);
             var gameVM = Mapper.Map<GameVM>(game);
             return Ok(gameVM);
         }

        // POST Games/new
        [Route("games/new")]
        public IHttpActionResult New(GameVM gameVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Mapper.CreateMap<GameVM, GameDTO>();
            var game = Mapper.Map<GameDTO>(gameVM);
            gameService.Create(game);
            return CreatedAtRoute("DefaultApi", new { id = game.GameKey }, gameVM);
        }

        //DELETE api/Games/5
        [ResponseType(typeof(GameVM))]
        [Route("games/delete/{key}")]
        public IHttpActionResult DeleteGame(string key)
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            var game = gameService.GetByKey(key);
            if (game == null)
            {
                return NotFound();
            }
            gameService.Remove(game);
            var gameVM = Mapper.Map<GameVM>(game);
            return Ok(gameVM);
        }

    }
}