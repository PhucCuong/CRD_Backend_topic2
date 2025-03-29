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
    public class MRes_Post:BaseModel.History
    {
        [Required]
        [Column("title")]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [Column("short_description")]
        [StringLength(255)]
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
    }
}
