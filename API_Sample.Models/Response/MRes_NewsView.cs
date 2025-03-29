using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_NewsView:BaseModel.History
    {
        [Column("news_id")]
        public int NewsId { get; set; }

        [Column("view_count")]
        public int? ViewCount { get; set; }
    }
}
