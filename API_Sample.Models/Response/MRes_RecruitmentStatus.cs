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
    public class MRes_RecruitmentStatus:BaseModel.History
    {
        [Column("application_id")]
        public int ApplicationId { get; set; }

        [Required]
        [Column("application_status")]
        [StringLength(20)]
        public string ApplicationStatus { get; set; }
    }
}
