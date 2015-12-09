using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.DAL.Entities
{
    public class PlatformType
    {
        public PlatformType()
        {
            this.Games = new HashSet<Game>();
        }

        [Key]
        public string Type { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}
