using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OnlineGameStore.BLL.Model
{
    public class GenreDTO
    {
        //public GenreDTO()
        //{
        //    this.Games = new HashSet<GameDTO>();
        //    this.Childrens = new HashSet<GenreDTO>();
        //}

        public string Name { get; set; }
        public string ParentGenre { get; set; }

      //  [JsonIgnore]
        public virtual GenreDTO Parent { get; set; }
     //   [JsonIgnore]
        public virtual ICollection<GenreDTO> Childrens { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<GameDTO> Games { get; set; }
    }
}
