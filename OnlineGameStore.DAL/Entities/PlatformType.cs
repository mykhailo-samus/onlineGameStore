using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OnlineGameStore.DAL.Entities
{
    public class PlatformType
    {
        //public PlatformType()
        //{
        //    this.Games = new HashSet<Game>();
        //}

        [Key]
        public string Type { get; set; }
       // [JsonIgnore]
        public virtual ICollection<Game> Games { get; set; }
    }
}
