using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Interfaces;

namespace OnlineGameStore.DAL.DBContext
{
    public partial class GameStoreContext : DbContext, IGameStoreContext
    {
        public GameStoreContext()
            : base("name=DbConnection")
        {
        }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<PlatformType> PlatformTypes { get; set; }
    }

    public class EntitiesContextInitializer : CreateDatabaseIfNotExists<GameStoreContext>
    {

        void InitializeGenres(GameStoreContext context)
        {
            List<Genre> genres = new List<Genre>
            {
                //genres
                new Genre { Name = "Strategy" },
                new Genre { Name = "RPG" },
                new Genre { Name = "Sports" },
                new Genre { Name = "Races" },
                new Genre { Name = "Action" },
                new Genre { Name = "Adventure" },
                new Genre { Name = "Puzzle&Skill" },
                new Genre { Name = "Misc" },
                //subgenres
                new Genre { Name = "RTS", ParentGenre = "Strategy"},
                new Genre { Name = "TBS", ParentGenre = "Strategy"},
                new Genre { Name = "Rally", ParentGenre = "Races"},
                new Genre { Name = "Arcade", ParentGenre = "Races"},
                new Genre { Name = "Formula", ParentGenre = "Races"},
                new Genre { Name = "Off-road", ParentGenre = "Races"},
                new Genre { Name = "FPS", ParentGenre = "Action"},
                new Genre { Name = "TPS", ParentGenre = "Action"}
            };
            foreach (var genre in genres)
            {
                context.Genres.Add(genre);
            }
        }

        void InitializePlatformTypes(GameStoreContext context)
        {
            List<PlatformType> platformTypes = new List<PlatformType>
            {
                new PlatformType { Type = "Mobile" },
                new PlatformType { Type = "Browser" },
                new PlatformType { Type = "Desktop" },
                new PlatformType { Type = "Console" }
            };
            foreach (var platformType in platformTypes)
            {
                context.PlatformTypes.Add(platformType);
            }
        }

        public Genre GetGenre(string name, GameStoreContext context)
        {
            return context.Genres.Find(name);
        }
        public PlatformType GetPlatform(string name, GameStoreContext context)
        {
            return context.PlatformTypes.Find(name);
        }


        void InitializeGames(GameStoreContext context)
        {
            List<Game> games = new List<Game>
            {
                new Game { GameKey = Guid.NewGuid().ToString(), Name = "Starcraft", Description = "StarCraft is a military science fiction real-time strategy video game developed and published by Blizzard Entertainment and released for Microsoft Windows on March 31, 1998."
                         , Genres = new List<Genre> { GetGenre("Strategy", context), GetGenre("RTS", context)}, PlatformTypes = new List<PlatformType> { GetPlatform("Desktop", context) } },
                
                new Game { GameKey = Guid.NewGuid().ToString(), Name = "Warcraft III", Description = "Warcraft III: Reign of Chaos is a high fantasy real-time strategy video game released by Blizzard Entertainment in July 2002."
                         , Genres = new List<Genre> { GetGenre("Strategy", context), GetGenre("RTS", context)}, PlatformTypes = new List<PlatformType> { GetPlatform("Desktop", context) } },

                new Game { GameKey = Guid.NewGuid().ToString(), Name = "Diablo II", Description = "Diablo II is an action role-playing hack and slash video game developed by Blizzard North and published by Blizzard Entertainment in 2000 for Microsoft Windows and Mac OS."
                         , Genres = new List<Genre> { GetGenre("RPG", context), GetGenre("Adventure", context)}, PlatformTypes = new List<PlatformType> { GetPlatform("Desktop", context), GetPlatform("Desktop", context) } },

                new Game { GameKey = Guid.NewGuid().ToString(), Name = "Deus Ex", Description = "Deus Ex is a cyberpunk-themed action-role playing video game—combining first-person shooter, stealth and role-playing elements—developed by Ion Storm and published by Eidos Interactive in 2000."
                         , Genres = new List<Genre> { GetGenre("Action", context), GetGenre("Adventure", context)}, PlatformTypes = new List<PlatformType> { GetPlatform("Desktop", context), GetPlatform("Desktop", context) } },
            };
            foreach (var game in games)
            {
                context.Games.Add(game);
            }
        }

        protected override void Seed(GameStoreContext context)
        {
            InitializeGenres(context);
            InitializePlatformTypes(context);
            InitializeGames(context);
        }

    }
}
