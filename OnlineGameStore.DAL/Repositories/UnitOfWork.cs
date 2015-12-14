using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.DAL.Repositories;
using System.Data.Entity;

namespace OnlineGameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;
        private ICommentRepository commentRepository;
        private IGameRepository gameRepository;
        public UnitOfWork(DbContext context, ICommentRepository commentRepository, IGameRepository gameRepository)
        {
            this.context = context;
            this.commentRepository = commentRepository;
            this.gameRepository = gameRepository;

        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
        public DbContext Context { get { return context; } }
        public IGameRepository GameRepository { get { return gameRepository; } }
        public ICommentRepository CommentRepository { get { return commentRepository; } }
    }
}
