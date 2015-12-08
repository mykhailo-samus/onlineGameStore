using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.DAL.Entities;
using System.Data.Entity;
using OnlineGameStore.DAL.Interfaces;

namespace OnlineGameStore.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
         private GameStoreContext context;

         public GameRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Game game)
        {
           context.Games.Add(game);
           context.SaveChanges();
        }


        public void Remove(Game game)
        {
            context.Games.Remove(game);
            context.SaveChanges();
        }

        public void Update(Game game)
        {
            context.Entry(game).State = EntityState.Modified;
            context.SaveChanges();
        }

        public Game GetByKey(string gameKey)
        {
                return context.Games.FirstOrDefault(s =>
                    s.GameKey.ToLower() == gameKey.ToLower());
        }

        public IEnumerable<Game> GetByGenre(string name)
        {
            return context.Games.Where(x => x.Genres.Any(y => y.Name == name));
        }

        public IEnumerable<Game> GetByPlatform(string type)
        {
            return context.Games.Where(x => x.PlatformTypes.Any(y => y.Type == type));
        }

        public IEnumerable<Game> GetAll()
        {
            return context.Games;
        }
    }
}
