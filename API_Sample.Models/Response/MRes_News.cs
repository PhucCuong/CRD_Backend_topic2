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
        [StringLength(255)]
        public string NewsName { get; set; }

        [Required]
        [Column("short_description")]
        [StringLength(255)]
        public string ShortDescription { get; set; }

        [Column("status")]
        public bool Status { get; set; }
    }
}
