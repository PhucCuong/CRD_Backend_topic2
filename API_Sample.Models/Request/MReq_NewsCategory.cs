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
    public class MReq_NewsCategory:BaseModel.History
    {
        [Key]
        [Column("news_category_id")]
        public int NewsCategoryId { get; set; }

        [Required]
        [Column("news_category_name")]
        [StringLength(255)]
        public string NewsCategoryName { get; set; }

        [Column("status")]
        public bool IsActive { get; set; }
    }
}
