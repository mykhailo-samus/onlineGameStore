using System;
using OnlineGameStore.DAL.Interfaces;

namespace OnlineGameStore.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get; }
        IGameStoreContext Context { get; }
        IGameRepository GameRepository { get; }
    }
}
