using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.Web.ViewModel;
using AutoMapper;
using System.Web.Script.Serialization;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using WebApi.OutputCache.V2;
using Serilog;

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
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [HttpGet]
        public IEnumerable<GameVM> GetGames()
        {
            Log.Debug("User requested GET: /games");
            return Mapper.Map<IEnumerable<GameVM>>(gameService.GetAll());
        }
        
         //GET /games/5
        [Route("games/{key}", Name = "GetGame")]
        [CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
        [HttpGet]
        public IHttpActionResult GetGame(string key)
        {
            Log.Debug("User requested GET: /games/{key}", key);
            var game = Mapper.Map<GameVM>(gameService.GetByKey(key));
            
            if (game == null)
            {
                Log.Error("Game with certain key is not found!");
                return NotFound();
            }

            return Ok(game);
        }

         //POST /games/update
         [Route("games/update")]
         [HttpPost] 
         public IHttpActionResult Update(GameVM gameVM)
         {
             Log.Debug("User requested POST: /games/update");
             var sourceGame = gameService.GetByKey(gameVM.GameKey);
             if (sourceGame == null)
             {
                 Log.Error("Game with certain key is not found!");
                 return NotFound();
             }

             if (!ModelState.IsValid)
             {
                 Log.Error("Request is not valid!");
                 return BadRequest(ModelState);
             }
             Mapper.Map(gameVM, sourceGame);
             
             gameService.Update(sourceGame);
             return CreatedAtRoute("GetGame", new { key = sourceGame.GameKey }, gameVM);
         }

        // POST games/new
        [Route("games/new")]
        [HttpPost]
        public IHttpActionResult New(GameVM gameVM)
        {
            Log.Debug("User requested POST: /games/new");
            if (!ModelState.IsValid)
            {
                Log.Error("Request is not valid!");
                return BadRequest(ModelState);
            }
            var game = Mapper.Map<GameDTO>(gameVM);
            gameService.Create(game);
            return CreatedAtRoute("GetGame", new { key = game.GameKey }, gameVM);
        }

        //POST games/remove
        [Route("games/remove")]
        [HttpPost]
        public IHttpActionResult Delete(GameVM gameVM)
        {
            Log.Debug("User requested POST: /games/remove");
            var game = gameService.GetByKey(gameVM.GameKey);
            if (game == null)
            {
                Log.Error("Game with certain key is not found!");
                return NotFound();
            }
            gameService.Remove(game);
            return Ok();
        }

        //POST games/{key}/download
        [Route("games/{key}/download")]
        [HttpGet]
        public HttpResponseMessage Download(string key)
        {
            Log.Debug("User requested GET: /games/{key}/remove", key);
            var game = gameService.GetByKey(key);

            if (game == null)
            {
               Log.Error("Game with certain key is not found!");
               return Request.CreateResponse(HttpStatusCode.NotFound, "Nonexistent game key! Please, try another value.");
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            response.Content = new StringContent(JsonConvert.SerializeObject(Mapper.Map<GameVM>(game)));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = String.Format("game_{0}.json", key)
            };

            return response;
        }
    }
}