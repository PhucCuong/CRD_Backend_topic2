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
    public class MRes_News : BaseModel.History
    {
        [Column("news_category_id")]
        public int NewsCategoryId { get; set; }

        [Required]
        [Column("news_name")]
        public string NewsName { get; set; }

        [Required]
        [Column("short_description")]
        public string ShortDescription { get; set; }

        [Column("status")]
        public bool IsActive { get; set; }

        [Required]
        [Column("content")]
        public string Content { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
