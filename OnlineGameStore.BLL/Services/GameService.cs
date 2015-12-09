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
            var game = Mapper.Map<Game>(GameDTO);
            db.GameRepository.Create(game);
        }


        public void Remove(GameDTO GameDTO)
        {
            var game = db.GameRepository.GetByKey(GameDTO.GameKey);
            Mapper.Map(GameDTO, game);
            db.GameRepository.Remove(game);
        }

        public void Update(GameDTO GameDTO)
        {
            var game = db.GameRepository.GetByKey(GameDTO.GameKey);
            Mapper.Map(GameDTO, game);
            db.GameRepository.Update(game);
        }

        public void Detach(GameDTO GameDTO)
        {
            var game = Mapper.Map<Game>(GameDTO);
            db.GameRepository.Detach(game);
        }

        public GameDTO GetByKey(string GameKey)
        {
            return Mapper.Map<GameDTO>(db.GameRepository.GetByKey(GameKey));
        }

        public IEnumerable<GameDTO> GetByGenre(string name)
        {
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetByGenre(name));
        }

        public IEnumerable<GameDTO> GetByPlatform(string type)
        {
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetByPlatform(type));
        }

        public IEnumerable<GameDTO> GetAll()
        {
            return Mapper.Map<IEnumerable<GameDTO>>(db.GameRepository.GetAll());
        }
    }
}
