using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OnlineGameStore.BLL.Model
{
    public class PlatformTypeDTO
    {
        //public PlatformTypeDTO()
        //{
        //    this.Games = new HashSet<GameDTO>();
        //}

        public string Type { get; set; }

      //  [JsonIgnore]
        public virtual ICollection<GameDTO> Games { get; set; }
    }
}
