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
    public class MRes_Post:BaseModel.History
    {
        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("field_id")]
        public int FieldId { get; set; }

        [Column("view_count")]
        public int? ViewCount { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Column("status")]
        public bool IsActive { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
