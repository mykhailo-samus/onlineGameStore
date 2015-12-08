using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.DAL.Entities;

using AutoMapper;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork db;
        public GameService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Create(GameDTO GameDTO)
        {
            Mapper.CreateMap<GameDTO, Game>();
            var game = Mapper.Map<Game>(GameDTO);
            db.GameRepository.Create(game);
        }


        public void Remove(GameDTO GameDTO)
        {
            Mapper.CreateMap<GameDTO, Game>();
            var game = Mapper.Map<Game>(GameDTO);
            db.GameRepository.Remove(game);
        }

        public void Update(GameDTO GameDTO)
        {
            Mapper.CreateMap<GameDTO, Game>();
            var game = Mapper.Map<Game>(GameDTO);
            db.GameRepository.Update(game);
        }

        public GameDTO GetByKey(string GameKey)
        {
            Mapper.CreateMap<Game, GameDTO>();
            return Mapper.Map<GameDTO>(db.GameRepository.GetByKey(GameKey));
        }

        public IEnumerable<GameDTO> GetByGenre(string name)
        {
            Mapper.CreateMap<Game, GameDTO>();
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetByGenre(name));
        }

        public IEnumerable<GameDTO> GetByPlatform(string type)
        {
            Mapper.CreateMap<Game, GameDTO>();
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetByPlatform(type));
        }

        public IEnumerable<GameDTO> GetAll()
        {
            Mapper.CreateMap<Game, GameDTO>();
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetAll());
        }
    }
}
