using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Genre> Genres { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlatformType> PlatformTypes { get; set; }
    }
}
