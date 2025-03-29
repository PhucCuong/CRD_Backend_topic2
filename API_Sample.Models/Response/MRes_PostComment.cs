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
    public class MRes_PostComment:BaseModel.History
    {
        [Column("post_id")]
        public int PostId { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [Column("comment_content")]
        public string CommentContent { get; set; }

        [Column("comment_at", TypeName = "datetime")]
        public DateTime CommentAt { get; set; }
    }
}
