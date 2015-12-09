using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGameStore.DAL.Entities
{
    public class Genre
    {
        //public Genre()
        //{
        //    this.Games = new HashSet<Game>();
        //    this.Childrens = new HashSet<Genre>();
        //}

        [Key]
        public string Name { get; set; }
        public string ParentGenre { get; set; }

        [ForeignKey("ParentGenre")]
        public virtual Genre Parent { get; set; }
        public virtual ICollection<Genre> Childrens { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
