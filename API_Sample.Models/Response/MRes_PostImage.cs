using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_PostImage:BaseModel.History
    {
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("image_url")]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Column("is_avatar")]
        public bool? IsAvatar { get; set; }
    }
}
