using System;
using OnlineGameStore.BLL.Model;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Interfaces
{
    public interface IGameService
    {
        void Create(GameDTO GameDTO);
        IEnumerable<GameDTO> GetAll();
        IEnumerable<GameDTO> GetByGenre(string name);
        GameDTO GetByKey(string GameKey);
        IEnumerable<GameDTO> GetByPlatform(string type);
        void Remove(GameDTO GameDTO);
        void Update(GameDTO GameDTO);
    }
}
