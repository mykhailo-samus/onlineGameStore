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


        protected override void Seed(GameStoreContext context)
        {

        }

    }
}
