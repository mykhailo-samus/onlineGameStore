using System;
using OnlineGameStore.BLL.Model;
using System.Collections.Generic;
namespace OnlineGameStore.BLL.Interfaces
{
    public interface ICommentService
    {
        void Create(CommentDTO commentDTO);
        IEnumerable<CommentDTO> GetCommentsByGameKey(string gameKey);
        void Remove(CommentDTO commentDTO);
        void Update(CommentDTO commentDTO);
    }
}
