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
        public GamesController(IGameService gameService)
        {
            this.gameService = gameService;
        }


        // GET /games
        [Route("games")]
        [HttpGet]
        public IEnumerable<GameVM> GetGames()
        {
            return Mapper.Map<IEnumerable<GameVM>>(gameService.GetAll());
        }
    
         //GET /games/5
        [Route("games/{key}", Name = "GetGame")]
        [HttpGet]
        public IHttpActionResult GetGame(string key)
        {
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

             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }
             Mapper.Map(gameVM, sourceGame);
             
             gameService.Update(sourceGame);
             return CreatedAtRoute("GetGame", new { key = sourceGame.GameKey }, gameVM);
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
            var game = Mapper.Map<GameDTO>(gameVM);
            gameService.Create(game);
            return CreatedAtRoute("GetGame", new { key = game.GameKey }, gameVM);
        }

        //POST api/Games/5
        [Route("games/remove")]
        [HttpPost]
        public IHttpActionResult Delete(GameVM gameVM)
        {
            var game = gameService.GetByKey(gameVM.GameKey);
            if (game == null)
            {
                return NotFound();
            }
            gameService.Remove(game);
            return Ok();
        }


    }
}