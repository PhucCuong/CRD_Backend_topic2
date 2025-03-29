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
    public class MRes_ContactLog:BaseModel.History
    {
        [Column("contact_id")]
        public int ContactId { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Column("contactLog_action")]
        [StringLength(255)]
        public string ContactLogAction { get; set; }

        [Column("notes")]
        [StringLength(1000)]
        public string Notes { get; set; }
    }
}
