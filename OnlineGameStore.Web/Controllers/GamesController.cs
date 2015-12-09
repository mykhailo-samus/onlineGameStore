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
        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }


        // GET /games
        [Route("games")]
        [HttpGet]
        public IEnumerable<GameVM> GetGames()
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            return Mapper.Map<IEnumerable<GameVM>>(gameService.GetAll());
        }
    
         //GET /games/5
        [Route("games/{key}")]
        [HttpGet]
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

         //POST /games/update
         [Route("games/update/")]
         [HttpPost]
         public IHttpActionResult Update(GameVM gameVM)
         {
             var sourceGame = gameService.GetByKey(gameVM.GameKey);
             if (sourceGame == null)
             {
                 return NotFound();
             }
             Mapper.CreateMap<GameDTO, GameVM>();
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
             var modifiedGame = Mapper.Map<GameVM, GameDTO>(gameVM);
             gameService.Update(modifiedGame);
             return StatusCode(HttpStatusCode.NoContent);
         }

        // POST Games/new
        [Route("games/new")]
        [HttpPost]
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

        //POST api/Games/5
        [Route("games/remove")]
        [HttpPost]
        public IHttpActionResult Delete(GameVM gameVM)
        {
            Mapper.CreateMap<GameDTO, GameVM>();
            var game = gameService.GetByKey(gameVM.GameKey);
            if (game == null)
            {
                return NotFound();
            }
            gameService.Remove(game);
            return Ok(gameVM);
        }


    }
}