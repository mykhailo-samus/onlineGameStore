using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineGameStore.DAL.DBContext;
using OnlineGameStore.DAL.Entities;
using System.Data.Entity;
using OnlineGameStore.DAL.Interfaces;

namespace OnlineGameStore.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
       private GameStoreContext context;
       public CommentRepository(GameStoreContext context)
        {
            this.context = context;
        }
       public void Create(Comment comment)
       {
           context.Comments.Add(comment);
           context.SaveChanges();
       }

       public void Remove(Comment comment)
       {
           context.Comments.Remove(comment);
           context.SaveChanges();
       }

       public void Update(Comment comment)
       {
           context.Entry(comment).State = EntityState.Modified;
           context.SaveChanges();
       }
       public IEnumerable<Comment> GetCommentsByGameKey(string gameKey)
       {
           return context.Comments.Where(x => x.GameKey == gameKey);
       }
    }
}
