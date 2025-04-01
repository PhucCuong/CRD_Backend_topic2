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
    public class MRes_NewsCategory:BaseModel.History
    {
        [Required]
        [Column("news_category_name")]
        [StringLength(255)]
        public string NewsCategoryName { get; set; }

        [Column("status")]
        public bool IsActive { get; set; }

        [Column("create_at", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_at", TypeName = "datetime")]
        public DateTime? UpdateAt { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
