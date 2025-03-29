using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API_Sample.Models.Common;

namespace API_Sample.Models.Response
{
    public class MRes_PostView:BaseModel.History
    {
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("view_count")]
        public int? ViewCount { get; set; }
    }
}
