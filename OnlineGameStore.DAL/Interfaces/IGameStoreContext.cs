using System;
using System.Collections.Generic;
using OnlineGameStore.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace OnlineGameStore.DAL.Interfaces
{
    public interface IGameStoreContext
    {
        DbSet<Comment> Comments { get; set; }
        DbSet<Game> Games { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<PlatformType> PlatformTypes { get; set; }
    }
}
