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
    public class MReq_RecruitmentStatus:BaseModel.History
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("application_id")]
        public int ApplicationId { get; set; }

        [Required]
        [Column("application_status")]
        [StringLength(20)]
        public string ApplicationStatus { get; set; }
    }
}
