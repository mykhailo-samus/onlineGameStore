using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OnlineGameStore.BLL.Model
{
    public class CommentDTO
    {
        //public CommentDTO()
        //{
        //    this.Childrens = new HashSet<CommentDTO>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? ParentId { get; set; }
        public string GameKey { get; set; }

        [JsonIgnore]
        public virtual ICollection<CommentDTO> Childrens { get; set; }
        [JsonIgnore]
        public virtual CommentDTO Parent { get; set; }
        [JsonIgnore]
        public virtual GameDTO Game { get; set; }
    }
}
