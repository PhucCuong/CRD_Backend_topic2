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
    public class MReq_FieldOfActivity:BaseModel.History
    {
        [Key]
        [Column("field_id")]
        public int FieldId { get; set; }

        [Required]
        [Column("field_name")]
        [StringLength(50)]
        public string FieldName { get; set; }

        [Column("status_")]
        public int IsActive { get; set; }

        [Column("create_at", TypeName = "datetime")]
        public DateTime? CreateAt { get; set; }

        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column("update_at", TypeName = "datetime")]
        public DateTime? UpdateAt { get; set; }

        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
    }
}
