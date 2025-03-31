using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Request
{
    public class MReq_Post:BaseModel.History
    {
        [Key]
        [Column("post_id")]
        public int PostId { get; set; }

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

        [Column("status")]
        public bool IsActive { get; set; }

        [Column("name_slug")]
        [StringLength(255)]
        public string NameSlug { get; set; }
    }

    public class MReq_PostPaging : PagingRequestBase
    {
        public string SequenceStatus { get; set; }

        public string SearchText { get; set; }
    }
}
