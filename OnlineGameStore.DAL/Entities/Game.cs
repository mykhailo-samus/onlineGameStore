using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.DAL.Entities
{
    public class Game
    {
        //public Game()
        //{
        //    this.Comments = new HashSet<Comment>();
        //    this.Genres = new HashSet<Genre>();
        //    this.PlatformTypes = new HashSet<PlatformType>();
        //}

        [Key]
        public string GameKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
