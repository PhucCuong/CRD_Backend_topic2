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
    public class MRes_AuditLog:BaseModel.History
    {
        [Required]
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [Column("action")]
        [StringLength(255)]
        public string Action { get; set; }

        [Column("entity_type")]
        [StringLength(50)]
        public string EntityType { get; set; }
    }
}
