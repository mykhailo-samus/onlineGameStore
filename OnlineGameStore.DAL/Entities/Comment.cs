using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OnlineGameStore.DAL.Entities
{
    public class Comment
    {
        //public Comment()
        //{
        //    this.Childrens = new HashSet<Comment>();
        //}

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public int? ParentId { get; set; }
        public string GameKey { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Childrens { get; set; }
        [JsonIgnore]
        public virtual Comment Parent { get; set; }
        [JsonIgnore]
        public virtual Game Game { get; set; }
    }
}
