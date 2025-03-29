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
    public class MRes_FeedBack:BaseModel.History
    {
        [Column("contact_id")]
        public int? ContactId { get; set; }

        [Column("rating")]
        public int? Rating { get; set; }

        [Required]
        [Column("comments")]
        [StringLength(1000)]
        public string Comments { get; set; }
    }
}
