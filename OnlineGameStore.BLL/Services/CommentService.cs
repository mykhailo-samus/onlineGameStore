using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Interfaces;
using OnlineGameStore.BLL.Model;
using OnlineGameStore.BLL.Interfaces;
using OnlineGameStore.DAL.Entities;
using AutoMapper;

namespace OnlineGameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork db;
        public CommentService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Create(CommentDTO commentDTO)
        {
            var comment = Mapper.Map<Comment>(commentDTO);
            db.CommentRepository.Create(comment);
        }

        public void Remove(CommentDTO commentDTO)
        {
            var comment = Mapper.Map<Comment>(commentDTO);
            db.CommentRepository.Remove(comment);
        }

        public void Update(CommentDTO commentDTO)
        {
            var comment = Mapper.Map<Comment>(commentDTO);
            db.CommentRepository.Update(comment);
        }
        public IEnumerable<CommentDTO> GetCommentsByGameKey(string gameKey)
        {
            return Mapper.Map<IEnumerable<CommentDTO>>(db.CommentRepository.GetCommentsByGameKey(gameKey));
        }
        public CommentDTO GetCommentById(int id)
        {
            return Mapper.Map<CommentDTO>(db.CommentRepository.GetCommentById(id));
        }
    }
}
