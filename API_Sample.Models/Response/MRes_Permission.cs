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
    public class MRes_Permission:BaseModel.History
    {
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [Column("action_name")]
        [StringLength(20)]
        public string ActionName { get; set; }

        [Column("allow_view")]
        public bool AllowView { get; set; }

        [Column("allow_create")]
        public bool AllowCreate { get; set; }

        [Column("allow_edit")]
        public bool AllowEdit { get; set; }

        [Column("alow_delete")]
        public bool AlowDelete { get; set; }
    }
}
