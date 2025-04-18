﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_PostShare:BaseModel.History
    {
        [Column("post_id")]
        public int PostId { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Column("share_at", TypeName = "datetime")]
        public DateTime ShareAt { get; set; }
    }
}
