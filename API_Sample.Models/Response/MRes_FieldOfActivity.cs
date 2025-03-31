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
    public class MRes_FieldOfActivity:BaseModel.History
    {
        [Required]
        [Column("field_name")]
        [StringLength(50)]
        public string FieldName { get; set; }

        [Column("status_")]
        public int IsActive { get; set; }
    }
}
