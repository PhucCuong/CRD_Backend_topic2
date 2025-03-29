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
    public class MRes_LoginHistory:BaseModel.History
    {
        [Column("login_at", TypeName = "datetime")]
        public DateTime LoginAt { get; set; }

        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
    }
}
