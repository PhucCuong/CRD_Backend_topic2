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
    public class MReq_NewsView:BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("news_id")]
        public int NewsId { get; set; }

        [Column("view_count")]
        public int? ViewCount { get; set; }
    }
}
