using System;
using System.Collections.Generic;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Interfaces
{
    public interface IGameRepository
    {
        void Create(Game game);
        IEnumerable<Game> GetAll();
        IEnumerable<Game> GetByGenre(string name);
        Game GetByKey(string gameKey);
        IEnumerable<Game> GetByPlatform(string type);
        void Remove(Game game);
        void Update(Game game);
    }
}
