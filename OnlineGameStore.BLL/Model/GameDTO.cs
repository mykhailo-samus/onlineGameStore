using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
namespace OnlineGameStore.BLL.Model
{
    public class GameDTO
    {
        //public GameDTO()
        //{
        //    this.Comments = new HashSet<CommentDTO>();
        //    this.Genres = new HashSet<GenreDTO>();
        //    this.PlatformTypes = new HashSet<PlatformTypeDTO>();
        //}

        public string GameKey { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

       // [JsonIgnore]
        public virtual ICollection<CommentDTO> Comments { get; set; }
      //  [JsonIgnore]
        public virtual ICollection<GenreDTO> Genres { get; set; }
       // [JsonIgnore]
        public virtual ICollection<PlatformTypeDTO> PlatformTypes { get; set; }
    }
}
