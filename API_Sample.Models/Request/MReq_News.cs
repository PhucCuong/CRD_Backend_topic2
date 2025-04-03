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
    public class MReq_News : BaseModel.History
    {
        [Key]
        [Column("news_id")]
        public int NewsId { get; set; }

        [Column("news_category_id")]
        public int NewsCategoryId { get; set; }

        [Required]
        [Column("news_name")]
        public string NewsName { get; set; }

        [Required]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }
        
        [Column("status")]
        public bool IsActive { get; set; }

        [Column("name_slug")]
        [StringLength(255)]
        public string NameSlug { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }

    }
}
