using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.DAL.Repositories;

namespace OnlineGameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGameStoreContext context;
        private ICommentRepository commentRepository;
        private IGameRepository gameRepository;
        public UnitOfWork(IGameStoreContext context, ICommentRepository commentRepository, IGameRepository gameRepository)
        {
            this.context = context;
            this.commentRepository = commentRepository;
            this.gameRepository = gameRepository;

        }
        public IGameStoreContext Context { get { return context; } }
        public IGameRepository GameRepository { get { return gameRepository; } }
        public ICommentRepository CommentRepository { get { return commentRepository; } }
    }
}
