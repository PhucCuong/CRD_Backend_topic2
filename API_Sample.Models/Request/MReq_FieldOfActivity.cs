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
        public int Status { get; set; }
    }
}
