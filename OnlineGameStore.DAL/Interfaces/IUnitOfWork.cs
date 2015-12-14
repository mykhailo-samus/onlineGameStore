using System;
using OnlineGameStore.DAL.Interfaces;
using System.Data.Entity;

namespace OnlineGameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        DbContext Context { get; }
        IGameRepository GameRepository { get; }
        void SaveChanges();
    }
}
