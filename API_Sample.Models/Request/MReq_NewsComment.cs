﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Request
{
    public class MReq_NewsComment:BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("news_id")]
        public int NewsId { get; set; }

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
