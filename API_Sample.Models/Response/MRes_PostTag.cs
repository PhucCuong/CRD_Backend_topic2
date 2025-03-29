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
    public class MRes_PostTag:BaseModel.History
    {
        [Column("post_id")]
        public int PostId { get; set; }

        [Required]
        [Column("tag_name")]
        [StringLength(50)]
        public string TagName { get; set; }
    }
}
