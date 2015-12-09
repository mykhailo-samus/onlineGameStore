using System;
using System.Collections.Generic;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Interfaces
{
    public interface ICommentRepository
    {
        void Create(Comment comment);
        IEnumerable<Comment> GetCommentsByGameKey(string gameKey);
        void Remove(Comment comment);
        void Update(Comment comment);
        Comment GetCommentById(int id);
    }
}
